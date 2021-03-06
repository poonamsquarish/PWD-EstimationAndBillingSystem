USE [PWD]
GO
/****** Object:  StoredProcedure [dbo].[SQSPGetLeadInKMData]    Script Date: 01-11-2021 12:23:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--  exec [SQSPGetLeadInKMData] 'cement','99.5',2

-- select LeadInKM,Group2LF,Group2FC from C2Manual where DSRId=2

ALTER procedure [dbo].[SQSPGetLeadInKMData] 
( @Material nvarchar(4000)
 ,@LeadInKM nvarchar(200)
 ,@DSRId nvarchar
)
AS
BEGIN

SET NOCOUNT ON;

--declare @Material nvarchar(4000)='Building rubish'
--declare @LeadInKM nvarchar(200) = 244.5, @DSRId nvarchar = 2
DECLARE @tableName nvarchar(50),@cols  nvarchar(50)
DECLARE @table nvarchar(50),@col1  nvarchar(50),@col2  nvarchar(50)

SELECT top 1  @tableName=[GroupType],
			@cols= (STUFF((SELECT ',' + [GroupName]
					FROM [PWD].[dbo].[MachineManualGroup]  a
					where a.[GroupType]=b.[GroupType]
					and GroupDescription LIKE '%'+@Material+'%' and DSRId = @DSRId
					FOR XML PATH('')
			), 1, 1, '') )
				FROM [PWD].[dbo].[MachineManualGroup]  b
				WHERE GroupDescription LIKE '%'+@Material+'%' and DSRId = @DSRId

select  
	@table=(Case 
	when @tableName='c1machine$' then 'C1Machine' 
	when @tableName='c1manual$' then 'C1Manual'
	when @tableName='c2manual$' then 'C2Manual'
	when @tableName='c2machine$' then 'C2Machine' else ''end)  ,

    @col1=SUBSTRING(@cols, 0, CHARINDEX(',', @cols)) ,
    @col2=SUBSTRING(@cols, CHARINDEX(',', @cols)  + 1, LEN(@cols)) 

	--select @table,@col1,@col2

DECLARE @FinalQuery nvarchar(max);
  
set @FinalQuery = 
 'DECLARE  @Group4LF VARCHAR(500) = 0;
  DECLARE  @LeadInKM varchar(200) = '+@LeadInKM+';
  DECLARE  @Group4FC VARCHAR(500) = 0;
  DECLARE  @LeadInKmPart varchar(4000) = 0;  
  DECLARE  @TotalGroup4LF VARCHAR(500) = 0;
  DECLARE  @TotalGroup4FC VARCHAR(500) = 0;
WHILE ( cast(@LeadInKM as decimal(18,3)) > 0 )'
+
'BEGIN '
+
	' SELECT top 1 @Group4LF= '+@col1+',@Group4FC= '+@col2+',@LeadInKmPart=max(LeadInKm) FROM '+@table+' WHERE LeadInKm <= @LeadInKM AND DSRId = '+@DSRId+' GROUP BY '+@col1+','+@col2+',LeadInKm order by LeadInKm desc;'
		+
		'set @TotalGroup4LF=cast(@TotalGroup4LF as decimal(18,3))+cast(@Group4LF as decimal(18,3));'
		+
		'set @TotalGroup4FC=cast(@TotalGroup4FC as decimal(18,3))+cast(@Group4FC as decimal(18,3));'
		+
		'set @LeadInKM=cast(@LeadInKM as decimal(18,3))-cast(@LeadInKmPart as decimal(18,3));'
		+
		' print cast(@LeadInKM as decimal(18,3))'
		+
' END'
		+
		' select '''+@table+'''as Type,cast(@TotalGroup4LF as decimal(18,3))  as LeadCharges union all'
		+
		' select '''+@table+'''as Type,cast(@TotalGroup4FC as decimal(18,3)) as LeadCharges ;'

	print @FinalQuery	
EXEC(@FinalQuery)


--===============================================================

--DECLARE 
--	@LeadInKM nvarchar(200) = 88,
--    @Group4LF VARCHAR(500)=0, 
--    @Group4FC   VARCHAR(500)=0,
--	@LeadInKmPart varchar(4000)=0,
--	@TotalGroup4LF VARCHAR(500)=0, 
--    @TotalGroup4FC   VARCHAR(500)=0;
	
--WHILE ( cast(@LeadInKM as decimal(18,3)) > 0 )
--BEGIN
--		SELECT top 1 @Group4LF=Group4LF,@Group4FC=Group4FC, @LeadInKmPart=max(LeadInKm) FROM C2Machine 
--		WHERE DSRId = @DSRId and LeadInKm <= @LeadInKM
--		GROUP BY Group4LF,Group4FC,LeadInKm order by LeadInKm desc

--		--print @LeadInKmPart

--		set @TotalGroup4LF=cast(@TotalGroup4LF as decimal(18,3))+cast(@Group4LF as decimal(18,3))

--		set @TotalGroup4FC=cast(@TotalGroup4FC as decimal(18,3))+cast(@Group4FC as decimal(18,3))
		
--		set @LeadInKM=cast(@LeadInKM as decimal(18,3))-cast(@LeadInKmPart as decimal(18,3))

--END
--			print cast(@TotalGroup4LF as decimal(18,3))
--			print cast(@TotalGroup4FC as decimal(18,3))





--===============================================================




--DECLARE @FinalQuery nvarchar(max);

--  DECLARE 
--    @GroupName VARCHAR(500), 
--    @GroupType   VARCHAR(500),
--	@GroupDescription varchar(4000);
 
--DECLARE cursor_LeadInKMCharges CURSOR
--FOR SELECT 
--      [GroupName]
--      ,[GroupType]
--      ,[GroupDescription]
     
--  FROM [PWD].[dbo].[MachineManualGroup]
--  WHERE GroupDescription LIKE '%'+@Material+'%' and DSRId = @DSRId
--OPEN cursor_LeadInKMCharges;
 
--FETCH NEXT FROM cursor_LeadInKMCharges INTO 
--    @GroupName, 
--    @GroupType,
--	@GroupDescription;
 
--WHILE @@FETCH_STATUS = 0
--    BEGIN
	
--        IF TRIM(@GroupType) = 'c1machine$'
--		BEGIN
--		--INSERT INTO #LeadInKMCharges
--		set @FinalQuery = CONCAT(@FinalQuery, ' SELECT ''c1machine'' as Type, GrossTotal as LeadCharges FROM C1Machine WHERE LeadInKm = '+@LeadInKM+' AND DSRId = '+@DSRId+' UNION ALL');
--		END
--		ELSE IF TRIM(@GroupType)  = 'c1manual$'
--		BEGIN
--		--INSERT INTO #LeadInKMCharges
		
--		set @FinalQuery = CONCAT(@FinalQuery,' SELECT ''c1manual'' as Type, GrossTotal as LeadCharges FROM C1Manual WHERE LeadInKm = '+@LeadInKM+' AND DSRId = '+@DSRId+' UNION ALL');
--		END
--		ELSE IF TRIM(@GroupType)  = 'c2manual$'
--		BEGIN
--		--INSERT INTO #LeadInKMCharges
--		set @FinalQuery = CONCAT(@FinalQuery, ' SELECT ''c2manual'' as Type, '+@GroupName+' as LeadCharges FROM C2Manual WHERE LeadInKm = '+@LeadInKM+' AND DSRId = '+@DSRId+'  UNION ALL');
--		END
--		ELSE IF TRIM(@GroupType)  = 'c2machine$'
--		BEGIN
--		--INSERT INTO #LeadInKMCharges
--		set @FinalQuery = CONCAT(@FinalQuery, ' SELECT ''c2machine'' as Type, '+@GroupName+' as LeadCharges FROM C2Machine WHERE LeadInKm = '+@LeadInKM+' AND DSRId = '+@DSRId+' UNION ALL');
--		END
		
--        FETCH NEXT FROM cursor_LeadInKMCharges INTO 
--            @GroupName, 
--    @GroupType,
--	@GroupDescription;
--    END;
 
--CLOSE cursor_LeadInKMCharges;
 
--DEALLOCATE cursor_LeadInKMCharges;
--print @FinalQuery
--set @FinalQuery = LEFT(@FinalQuery,LEN(@FinalQuery)-CHARINDEX('U', REVERSE(@FinalQuery),0));
--EXEC(@FinalQuery)

--===============================================================


END




