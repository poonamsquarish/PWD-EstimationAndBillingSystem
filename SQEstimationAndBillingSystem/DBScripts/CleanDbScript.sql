---clean database
delete from c1machine			where DSRId<>2
delete from c2machine			where DSRId<>2
delete from C1Manual			where DSRId<>2
delete from C2Manual			where DSRId<>2
delete from FloorRates			where DSRId<>2
delete from M_SpecificRates		where DSRId<>2
delete from LabourInsurance		where DSRId<>2
delete from M_MaterialRates		
delete from M_Materials			where DSRId<>2
delete from M_LabourRate		where DSRId<>2
delete from MachineManualGroup	where DSRId<>2
delete from AbstractSheet		where DSRId<>2
delete from MeasurementSheet	where DSRId<>2
delete from LeadCharges			where DSRId<>2
delete from ProjectDetails		where DSRId<>2
delete from DSRDetails			where DSRId<>2
delete from [DSR]				where Id<>2

dbcc checkident('c1machine',reseed,1)
dbcc checkident('c2machine',reseed,1)
dbcc checkident('C1Manual',reseed,1)
dbcc checkident('C2Manual',reseed,1)
dbcc checkident('LeadCharges',reseed,1)
dbcc checkident('AbstractSheet',reseed,1)
dbcc checkident('MeasurementSheet',reseed,1)
dbcc checkident('M_SpecificRates',reseed,1)
dbcc checkident('M_Materials',reseed,1)
dbcc checkident('LabourInsurance',reseed,1)
dbcc checkident('M_MaterialRates',reseed,1)
dbcc checkident('M_LabourRate',reseed,1)
dbcc checkident('MachineManualGroup',reseed,1)
dbcc checkident('ProjectDetails',reseed,1)
dbcc checkident('DSRDetails',reseed,1)
dbcc checkident('[DSR]',reseed,1)
