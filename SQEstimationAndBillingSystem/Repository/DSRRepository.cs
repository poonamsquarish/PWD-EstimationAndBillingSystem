using CsvHelper;
using Newtonsoft.Json;
using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SQEstimationAndBillingSystem.Repository
{
    public class DSRRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public string connectionString = string.Empty;
        public string fileName = string.Empty;
        public string localFileName = string.Empty;
        public bool IsErrorOccured = false;

        public DSRRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }
        public string InsertData(int DSRId)
        {
            var ResultDsr = CheckDSRExists();
            if(ResultDsr > 0)
            _dbContext.Database.SqlQuery<string>("SQSPDeleteAllProjectsByDsrId @DSRId", new SqlParameter("DSRId", ResultDsr)).First();
            return Upload();
        }
        public string InsertData1()
        {
            string working_Directory = ConfigurationManager.AppSettings["DSRUrl"];
            WriteToFile($"InsertData Started{Environment.NewLine}");
            FileInfo localFileInfo = null;
            try
            {
                //string filePath = "c:\\";


                if (!Directory.Exists(working_Directory))
                {
                    WriteToFile($"Working_Directory doesn't exists on local Server: {working_Directory}{Environment.NewLine}");
                    return "Directory doesn't exists.";
                }

                var directory = new DirectoryInfo(working_Directory);
                var filePath = "";
                foreach (var item in directory.GetFiles())
                {
                    filePath = item.FullName;
                }
                localFileInfo = new FileInfo(filePath);

                if (!localFileInfo.Exists)
                    return "DSR not found.";

                string localFileName = localFileInfo.Name;
                WriteToFile($"InsertData started with file name : {localFileName}{Environment.NewLine}");

                //string fileName = Regex.Replace(Path.GetFileNameWithoutExtension(localFileInfo.Name), @"[\d-]", string.Empty).Replace("_", string.Empty);

                using (StreamReader reader = new StreamReader(filePath))
                {
                    using (CsvReader csv = new CsvReader(reader))
                    {
                        csv.Configuration.MissingFieldFound = null;
                        csv.Configuration.BadDataFound = null;
                        csv.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;
                        //csv.Configuration.HeaderValidated = null;
                        //csv.Configuration.HasHeaderRecord = false;
                        if (localFileName.StartsWith("SSR"))
                        {
                            // csv.Configuration.PrepareHeaderForMatch = (header,index) => Regex.Replace(header, "[\\s/]", string.Empty);
                            var records = csv.GetRecords<DSRModelUpload>()?.ToList() ?? null;

                            if (records?.Count == 0)
                                return "No record found.";

                            if (!PreInsertOpertation(localFileName))
                            {
                                BulkInsert(localFileName, "DSRDetails", ToDataTable(records));
                            }
                            else
                            {
                                return "DSR already uploaded.";
                            }
                        }

                        //List<string> result = new List<string>();
                        //string value;
                        //while (csv.Read())
                        //{
                        //    for (int i = 0; csv.TryGetField<string>(i, out value); i++)
                        //    {
                        //        result.Add(value);
                        //    }
                        //}

                    }
                }

                if (!IsErrorOccured)
                {


                    WriteToFile($"{fileName} with FileName : {localFileName} completed.{Environment.NewLine}");

                }
                else
                {
                    WriteToFile($"{fileName} with FileName : {localFileName} failed.{Environment.NewLine}");

                }

            }
            catch (Exception ex)
            {
                WriteToFile($"{fileName} with FileName : {localFileName} failed.{Environment.NewLine}");
                string info = $"Exception Occured while Getting File List: {Environment.NewLine}{JsonConvert.SerializeObject(ex)}{Environment.NewLine}";
                WriteToFile(info);
                IsErrorOccured = true;

            }
            finally
            {
                WriteToFile($"InsertData Completed{Environment.NewLine}");
            }

            return "Successful";
        }

        public List<DSRModel> GetAllDSRDetails(long DSRId)
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetAllDSRDetails @DSRId", new SqlParameter("DSRId", DSRId)).ToList();
        }

        public List<DSRFileModel> GetAllDSR()
        {
            return _dbContext.Database.SqlQuery<DSRFileModel>("SQSPGetAllDSR").ToList();
        }

        public List<ProjectDetailsModel> GetAllProjectsOfDSR(long dsrid)
        {
            return _dbContext.Database.SqlQuery<ProjectDetailsModel>("SQSPGetAllProjectsOfDSR @ID", new SqlParameter("ID", dsrid)).ToList();
        }

        public void BulkInsert(string fileName, string tableName, DataTable dataTable)
        {
            WriteToFile($"BulkInsert Started{Environment.NewLine}");
            long NoOfRecordsToCopy = dataTable.Rows.Count;
            long NoOfRecordsCopiedSuccessfully = 0;
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["SQEstimationAndBillingSystemDbForBulkInsert"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SQSPAddDSR";
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = fileName;
                    //cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = userId;
                    cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@DSRName", SqlDbType.NVarChar).Value = string.Concat("DSR", " ", DateTime.Now.Year);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    string id = cmd.Parameters["@id"].Value.ToString();
                    DataColumn DSRFKColumn = new DataColumn();
                    DSRFKColumn.ColumnName = "DSRID";
                    DSRFKColumn.DefaultValue = Convert.ToInt16(id);
                    dataTable.Columns.Add(DSRFKColumn);
                    connection.Close();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        var filesInserted = 0L;
                        bulkCopy.BulkCopyTimeout = Int32.MaxValue;
                        connection.Open();
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.NotifyAfter = 1;
                        bulkCopy.SqlRowsCopied += (s, e) => filesInserted = e.RowsCopied; // added code as per sagar mail to keep log of records
                        try
                        {
                            bulkCopy.ColumnMappings.Add(0, 1);
                            bulkCopy.ColumnMappings.Add(1, 2);
                            bulkCopy.ColumnMappings.Add(2, 3);
                            bulkCopy.ColumnMappings.Add(3, 4);
                            bulkCopy.ColumnMappings.Add(4, 5);
                            bulkCopy.ColumnMappings.Add(5, 6);
                            bulkCopy.ColumnMappings.Add(6, 7);
                            bulkCopy.ColumnMappings.Add(7, 8);
                            bulkCopy.ColumnMappings.Add(8, 9);
                            bulkCopy.ColumnMappings.Add(9, 10);
                            bulkCopy.WriteToServer(dataTable);
                            NoOfRecordsCopiedSuccessfully = filesInserted;
                        }
                        catch (Exception ex)
                        {
                            string info = $"Exception Occured while performing Bulk Insert :{Environment.NewLine}{JsonConvert.SerializeObject(ex)}{Environment.NewLine}";
                            IsErrorOccured = true;
                            WriteToFile(info);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                WriteToFile("File name: " + fileName);
                WriteToFile("No Of Records To Copy: " + NoOfRecordsToCopy);
                WriteToFile("No Of Records Copied Successfully: " + NoOfRecordsCopiedSuccessfully);
                WriteToFile("No Of Records Failed: " + (NoOfRecordsToCopy - NoOfRecordsCopiedSuccessfully));
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public bool PreInsertOpertation(string FileName)
        {
            WriteToFile($"PreInsertOpertation Started{Environment.NewLine}");
            try
            {

                var result = _dbContext.Database.SqlQuery<string>("SQPPreInsertOpertation @FileName", new SqlParameter("@FileName", FileName)).FirstOrDefault();
                if (result == "true")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string info = $"Exception Occured while performing Pre-Insert Opertation: {Environment.NewLine}{JsonConvert.SerializeObject(ex)}{Environment.NewLine}";
                WriteToFile(info);

                return false;
            }
            finally
            {
                WriteToFile($"PreInsertOpertation Ended{Environment.NewLine}");
            }
        }

        public void MoveFiles(string sourceFile, string destinationFile, string localFileName)
        {
            if (!Directory.Exists(destinationFile))
            {
                Directory.CreateDirectory(destinationFile);
            }

            File.Copy(sourceFile, Path.Combine(destinationFile, localFileName), true);
            //File.Delete(sourceFile);
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        public DSRModel GetDSRDetailsById(long id)
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetDSRDetailsById @ID", new SqlParameter("ID", id)).First();
        }

        public string DeleteDSRDetailsById(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteDSRDetailsById @ID", new SqlParameter("ID", id)).First();
        }

        public string AddEditDSRDetails(DSRModel model)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPAddEditDSRDetails @ID,@SrNo,@Chapter,@SSRItemNo, @ReferenceNo,@DescriptionOfTheItem,@AdditionalSpecification,@Unit,@CompletedRateExcludingGSTInRs,@LabourRateExcludingGSTInRs,@IsNonDSRItem,@DSRId,@CreatedBy,@ModifiedBy",
              new SqlParameter("ID", model.id),
              new SqlParameter("SrNo", "0"),
              new SqlParameter("Chapter", model.Chapter),
              new SqlParameter("SSRItemNo", model.SSRItemNo),
              new SqlParameter("ReferenceNo", model.ReferenceNo),
               new SqlParameter("DescriptionOfTheItem", model.DescriptionOfTheItem),
              new SqlParameter("AdditionalSpecification", model.AdditionalSpecification),
              new SqlParameter("Unit", model.Unit),
              new SqlParameter("CompletedRateExcludingGSTInRs", model.CompletedRateExcludingGSTInRs),
              new SqlParameter("LabourRateExcludingGSTInRs", model.LabourRateExcludingGSTInRs),
              new SqlParameter("IsNonDSRItem", model.IsNonDSRItem),
              new SqlParameter("DSRId", model.DSRId),
              new SqlParameter("CreatedBy", model.CreatedBy),
              new SqlParameter("ModifiedBy", model.ModifiedBy)

                ).FirstOrDefault();

            //return "Success";
        }
        protected string Upload()
        {
            try
            {
                //Upload and save the file
                //string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                //FileUpload1.SaveAs(excelPath);
                string working_Directory = ConfigurationManager.AppSettings["DSRUrl"];
                WriteToFile($"InsertData Started{Environment.NewLine}");
                FileInfo localFileInfo = null;

                if (!Directory.Exists(working_Directory))
                {
                    WriteToFile($"Working_Directory doesn't exists on local Server: {working_Directory}{Environment.NewLine}");
                    return "Directory doesn't exists.";
                }

                var directory = new DirectoryInfo(working_Directory);
                var filePath = "";
                foreach (var item in directory.GetFiles())
                {
                    filePath = item.FullName;
                }
                localFileInfo = new FileInfo(filePath);

                if (!localFileInfo.Exists)
                    return "DSR not found.";

                string localFileName = localFileInfo.Name;
                WriteToFile($"InsertData started with file name : {localFileName}{Environment.NewLine}");


                string conString = string.Empty;
                string extension = Path.GetExtension(filePath);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;

                }
                conString = string.Format(conString, filePath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    DataTable SheetNames = new DataTable();
                    excel_con.Open();
                    // string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    SheetNames = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    String[] excelSheets = new String[SheetNames.Rows.Count];

                    int i = 0;
                    foreach (DataRow item in SheetNames.Rows)
                    {
                        if (item["TABLE_NAME"].ToString().Contains("$"))
                        {
                            excelSheets[i] = item["TABLE_NAME"].ToString();
                        }
                        i++;
                    }
                    DataTable dtExcelData = new DataTable();


                    long insertedDSRId = 0;
                    string IsDSRExists = "false";
                    if (excelSheets.Distinct().ToArray().Length > 0)
                    {
                        string myConnectionString = ConfigurationManager.ConnectionStrings["SQEstimationAndBillingSystemDbForBulkInsert"].ConnectionString;
                        SqlConnection conn = new SqlConnection(myConnectionString);
                        conn.Open();

                        //string qry = @"INSERT INTO [dbo].[DSR] (DSRName,[FileName],[CreatedOn]) VALUES ('" + string.Concat("DSR", " ", DateTime.Now.Year) + "','" + filePath + "','" + DateTime.Now + "') select @@identity";
                        //SqlCommand cmd = new SqlCommand();
                        //cmd = new SqlCommand(qry, conn);
                        //IsDSRExists = cmd.ExecuteScalar().ToString();

                        string qry = @"INSERT INTO [dbo].[DSR] (DSRName,[FileName],[CreatedOn]) VALUES ('"+ string.Concat("DSR", " ", DateTime.Now.Year) + "','" + filePath + "','" + DateTime.Now + "') select @@identity";
                        SqlCommand cmd = new SqlCommand();
                        cmd = new SqlCommand(qry, conn);
                        insertedDSRId = Convert.ToInt64(cmd.ExecuteScalar());
                        conn.Close();
                    }

                    foreach (var item in excelSheets.Distinct().ToArray())
                    {
                        if (item != null)
                        {

                            if (item.ToLower() == "c1machine$" || item.ToLower() == "c2machine$" || item.ToLower() == "c1manual$" || item.ToLower() == "c2manual$" || item.ToLower() == "'labour rate$'" || item.ToLower() == "'material rates$'" || item.ToLower() == "ssr$" || item.ToLower() == "specificrates$" || item.ToLower() == "labourinsurance$" || item.ToLower() == "floorrates$")
                            {
                                // dtExcelData = new DataTable();

                                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + item + "]", excel_con))
                                {
                                    dtExcelData = new DataTable();
                                    dtExcelData.Columns.Add("DSRId");
                                    dtExcelData.Columns["DSRId"].DataType = typeof(Int64);
                                    dtExcelData.Columns["DSRId"].DefaultValue = insertedDSRId;
                                    
                                    oda.Fill(dtExcelData);

                                }
                                //C2 manual,C2 machine,index,corrected foot note

                                string consString = ConfigurationManager.ConnectionStrings["SQEstimationAndBillingSystemDbForBulkInsert"].ConnectionString;
                                using (SqlConnection con = new SqlConnection(consString))
                                {

                                    if (item.ToLower() == "c1machine$" || item.ToLower() == "c2machine$" || item.ToLower() == "c1manual$" || item.ToLower() == "c2manual$")
                                    {
                                        foreach (var col in dtExcelData.Columns)
                                        {
                                            string qry = @"INSERT INTO [dbo].[MachineManualGroup] ([GroupName],[GroupType],[GroupDescription],[DSRId]) VALUES ('" + col.ToString() + "','" + item.ToLower() + "','" + dtExcelData.Rows[0][col.ToString()] + "','" + insertedDSRId + "')";
                                            SqlCommand cmd = new SqlCommand();
                                            cmd = new SqlCommand(qry, con);
                                            con.Open();
                                            cmd.ExecuteNonQuery();

                                            foreach (var x in dtExcelData.Rows[0][col.ToString()].ToString().Split(','))
                                            {
                                                if (col.ToString().Contains("Group") && col.ToString().Contains("FC"))
                                                {
                                                    qry = @"INSERT INTO [dbo].[M_Materials] ([GroupName],[GroupType],[Material],[Unit],[DSRId]) VALUES ('" + col.ToString() + "','" + item.ToLower() + "','" + x.Trim() + "','" + dtExcelData.Rows[1][col.ToString()].ToString().Trim() + "','" + insertedDSRId + "')";
                                                    cmd = new SqlCommand();
                                                    cmd = new SqlCommand(qry, con);
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                             

                                            con.Close();
                                        }

                                    }


                                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                                    {
                                        con.Open();
                                        dtExcelData = dtExcelData.Rows
                                                                   .Cast<DataRow>()
                                                                   .Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string)))
                                                                   .CopyToDataTable();




                                        if (item.ToLower().Contains("material rates"))
                                        {
                                            //Set the database table name
                                            sqlBulkCopy.DestinationTableName = "dbo.M_MaterialRates";
                                            dtExcelData.Columns.RemoveAt(1);
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("labour rate"))
                                        {
                                            sqlBulkCopy.DestinationTableName = "dbo.M_LabourRate";
                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                                            sqlBulkCopy.ColumnMappings.Add("Per Unit", "Unit");
                                            sqlBulkCopy.ColumnMappings.Add("Rate", "Rate");
                                            dtExcelData.Columns.RemoveAt(1);
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("specificrates"))
                                        {
                                            sqlBulkCopy.DestinationTableName = "dbo.M_SpecificRates";

                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("SpecificAreas", "Description");
                                            sqlBulkCopy.ColumnMappings.Add("PercentageInRates", "PercentageInRates");
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("floorrates"))
                                        {
                                            sqlBulkCopy.DestinationTableName = "dbo.FloorRates";
                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("Floor", "Floor");
                                            sqlBulkCopy.ColumnMappings.Add("Percentage", "Percentage");
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("labourinsurance"))
                                        {
                                            sqlBulkCopy.DestinationTableName = "dbo.LabourInsurance";
                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                                            sqlBulkCopy.ColumnMappings.Add("Percentage", "Percentage");
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("c1machine"))
                                        {
                                            //dtExcelData.Columns.Cast<DataColumn>().ToList().ForEach(x =>
                                            //    sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(x.ColumnName, x.ColumnName)));


                                            sqlBulkCopy.DestinationTableName = "dbo.C1Machine";

                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("Lead in Km", "LeadInKm");
                                            sqlBulkCopy.ColumnMappings.Add("Av#Speed Km/Hrs", "AvgSpeedKmHrs");
                                            sqlBulkCopy.ColumnMappings.Add("Factor", "Factor");
                                            sqlBulkCopy.ColumnMappings.Add("No Of Trips", "NoOfTrips");
                                            sqlBulkCopy.ColumnMappings.Add("Km done", "KmDone");
                                            sqlBulkCopy.ColumnMappings.Add("Qty of Diesel=     4Km/Ltr", "QtyOfDiesel4KmPerLtr");
                                            sqlBulkCopy.ColumnMappings.Add("Cost of Diesel", "CostOfDiesel");
                                            sqlBulkCopy.ColumnMappings.Add("Qty of mobile oil in Ltr", "QtyOfMobileOilInLtr");
                                            sqlBulkCopy.ColumnMappings.Add("Cost of mobile oil@270/Ltr", "CostOfMobileOil270PerLtr");
                                            sqlBulkCopy.ColumnMappings.Add("Hire Charges of Truck@ Rs# 2200 per Day", "HireChargesOfTruck");
                                            sqlBulkCopy.ColumnMappings.Add("Total Cost", "TotalCost");
                                            sqlBulkCopy.ColumnMappings.Add("add 10% Overhead Charges", "AddOverheadCharges");
                                            sqlBulkCopy.ColumnMappings.Add("Gross Total of 12+13", "GrossTotal");
                                            sqlBulkCopy.ColumnMappings.Add("Cost per Trip=(Col 13/4)", "CostPerTrip");
                                            sqlBulkCopy.ColumnMappings.Add("Cost of loader per trip of tipper", "CostOfLoaderPerTripOfTipper");
                                            sqlBulkCopy.ColumnMappings.Add("Total Cost Per Trip", "TotalCostPerTrip");

                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("c1manual"))
                                        {
                                            
                                            //  MachineManualGroup
                                            sqlBulkCopy.DestinationTableName = "dbo.C1Manual";
                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("Lead in Km", "LeadInKm");
                                            sqlBulkCopy.ColumnMappings.Add("Av#Speed Km/Hrs", "AvgSpeedKmHrs");
                                            sqlBulkCopy.ColumnMappings.Add("Factor", "Factor");
                                            sqlBulkCopy.ColumnMappings.Add("No# of Trips", "NoOfTrips");
                                            sqlBulkCopy.ColumnMappings.Add("Km done", "KmDone");
                                            sqlBulkCopy.ColumnMappings.Add("Qty of Diesel=     4Km/Ltr", "QtyOfDiesel4KmPerLtr");
                                            sqlBulkCopy.ColumnMappings.Add("Cost of Diesel", "CostOfDiesel");
                                            sqlBulkCopy.ColumnMappings.Add("Qty of mobile oil in Ltr", "QtyOfMobileOilInLtr");
                                            sqlBulkCopy.ColumnMappings.Add("Cost of mobile oil@270/Ltr", "CostOfMobileOil270PerLtr");
                                            sqlBulkCopy.ColumnMappings.Add("Cost of 6 Mazdoor @448/lbr", "HireChargesOfMazdoor");
                                            sqlBulkCopy.ColumnMappings.Add("Hire Charges of Truck@Rs# 2200 per day", "HireChargesOfTruck");
                                            sqlBulkCopy.ColumnMappings.Add("Total cost", "TotalCost");
                                            sqlBulkCopy.ColumnMappings.Add("add 10% Overhead Charges", "AddOverheadCharges");
                                            sqlBulkCopy.ColumnMappings.Add("Gross Total of 12+13", "GrossTotal");
                                            sqlBulkCopy.ColumnMappings.Add("Cost per Trip=(Col 14/4)", "CostPerTrip");
                                            sqlBulkCopy.ColumnMappings.Add("Fuel   Componant", "FuelComponent");
                                            sqlBulkCopy.ColumnMappings.Add("Cost Per trip 10 MT", "CostPerTrip10MT");
                                            dtExcelData.Rows.RemoveAt(0);
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("c2machine"))
                                        {
                                            
                                            sqlBulkCopy.DestinationTableName = "dbo.C2Machine";
                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("Lead In Km", "LeadInKm");
                                            sqlBulkCopy.ColumnMappings.Add("Total Cost Per Trip Lead Charges (Including lodar)", "CostPerTripLeadCharges");
                                            sqlBulkCopy.ColumnMappings.Add("Fuel Componant", "FuelComponent");
                                            sqlBulkCopy.ColumnMappings.Add("Group1LF", "Group1LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group1FC", "Group1FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group2LF", "Group2LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group2FC", "Group2FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group3LF", "Group3LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group3FC", "Group3FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group4LF", "Group4LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group4FC", "Group4FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group5LF", "Group5LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group5FC", "Group5FC");
                                            dtExcelData.Rows.RemoveAt(0);
                                            dtExcelData.Rows.RemoveAt(0);
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("c2manual"))
                                        {
                                           
                                            sqlBulkCopy.DestinationTableName = "dbo.C2Manual";
                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("Lead In Km", "LeadInKm");
                                            sqlBulkCopy.ColumnMappings.Add("Cost Per Trip Lead Charges", "CostPerTripLeadCharges");
                                            sqlBulkCopy.ColumnMappings.Add("Fuel Componant", "FuelComponant");
                                            sqlBulkCopy.ColumnMappings.Add("Group1LF", "Group1LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group1FC", "Group1FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group2LF", "Group2LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group2FC", "Group2FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group3LF", "Group3LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group3FC", "Group3FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group4LF", "Group4LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group4FC", "Group4FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group5LF", "Group5LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group5FC", "Group5FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group6LF", "Group6LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group6FC", "Group6FC");
                                            sqlBulkCopy.ColumnMappings.Add("Group7LF", "Group7LF");
                                            sqlBulkCopy.ColumnMappings.Add("Group7FC", "Group7FC");
                                            dtExcelData.Rows.RemoveAt(0);
                                            dtExcelData.Rows.RemoveAt(0);
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }
                                        else if (item.ToLower().Contains("ssr"))
                                        {
                                            sqlBulkCopy.ColumnMappings.Add("DSRId", "DSRId");
                                            sqlBulkCopy.ColumnMappings.Add("Sr No", "SrNo");
                                            sqlBulkCopy.ColumnMappings.Add("Chapter", "Chapter");
                                            sqlBulkCopy.ColumnMappings.Add("SSR Item No", "SSRItemNo");
                                            sqlBulkCopy.ColumnMappings.Add("Reference No", "ReferenceNo");
                                            sqlBulkCopy.ColumnMappings.Add("Description Of The Item", "DescriptionOfTheItem");
                                            sqlBulkCopy.ColumnMappings.Add("Additional Specification", "AdditionalSpecification");
                                            sqlBulkCopy.ColumnMappings.Add("Unit", "Unit");
                                            sqlBulkCopy.ColumnMappings.Add("Completed Rate Excluding GST In Rs", "CompletedRateExcludingGSTInRs");
                                            sqlBulkCopy.ColumnMappings.Add("Labour Rate Excluding GST In Rs", "LabourRateExcludingGSTInRs");

                                            sqlBulkCopy.DestinationTableName = "dbo.DSRDetails";
                                            sqlBulkCopy.WriteToServer(dtExcelData);
                                        }

                                        //[OPTIONAL]: Map the Excel columns with that of the database table
                                        //sqlBulkCopy.ColumnMappings.Add("Id", "PersonId");
                                        //sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                                        //sqlBulkCopy.ColumnMappings.Add("Salary", "Salary");

                                        con.Close();
                                    }
                                }
                            }

                        }
                    }

                    excel_con.Close();
                }
            }
            catch (Exception ex)
            {

                WriteToFile(ex.InnerException.ToString());
                return "Failed";
            }

            return "Success";
        }

        public int CheckDSRExists()
        {

            string working_Directory = ConfigurationManager.AppSettings["DSRUrl"];
            WriteToFile($"InsertData Started{Environment.NewLine}");
            FileInfo localFileInfo = null;

            if (!Directory.Exists(working_Directory))
            {
                WriteToFile($"Working_Directory doesn't exists on local Server: {working_Directory}{Environment.NewLine}");
                return 0;
            }

            var directory = new DirectoryInfo(working_Directory);
            var filePath = "";
            foreach (var item in directory.GetFiles())
            {
                filePath = item.FullName;
            }

          var result =   _dbContext.Database.SqlQuery<int>("SQSPIsDSRPresent @DSRName,@FileName", new SqlParameter("DSRName", string.Concat("DSR", " ", DateTime.Now.Year)), new SqlParameter("FileName", filePath)).First();

            return result;
        }
    }
}