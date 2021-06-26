using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace SquarishSQEstimationAndBillingSystem.Helper
{
    public static class MultipleResultSets
    {
        public static MultipleResultSetWrapper MultipleResults(this DbContext db, string storedProcedure, List<Tuple<string, object>> parameterList)
        {
            return new MultipleResultSetWrapper(db, storedProcedure, parameterList);
        }

        public class MultipleResultSetWrapper
        {
            private readonly DbContext _db;
            private readonly string _storedProcedure;
            private readonly List<Tuple<string, object>> _parameterList;
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> _resultSets;

            public MultipleResultSetWrapper(DbContext db, string storedProcedure, List<Tuple<string, object>> parameterList)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                _parameterList = parameterList;
                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                _resultSets.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }

            public List<IEnumerable> Execute()
            {
                var results = new List<IEnumerable>();

                using (DbConnection connection = _db.Database.Connection)
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = "EXEC " + _storedProcedure;

                    foreach (var item in _parameterList)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = item.Item1;
                        parameter.Value = item.Item2;

                        command.Parameters.Add(parameter);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        var adapter = ((IObjectContextAdapter)_db);
                        foreach (var resultSet in _resultSets)
                        {
                            results.Add(resultSet(adapter, reader));
                            reader.NextResult();
                        }
                    }

                    return results;
                }
            }
        }
    }
}