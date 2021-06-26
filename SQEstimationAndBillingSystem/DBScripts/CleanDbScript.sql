---clean database
delete from c1machine
delete from c2machine
delete from C1Manual
delete from C2Manual
delete from FloorRates
delete from M_SpecificRates
delete from LabourInsurance
delete from M_MaterialRates
delete from M_LabourRate
delete from MachineManualGroup
delete from AbstractSheet
delete from MeasurementSheet
delete from LeadCharges
delete from DSRDetails
delete from [DSR]

dbcc checkident('c1machine',reseed,1)
dbcc checkident('c2machine',reseed,1)
dbcc checkident('C1Manual',reseed,1)
dbcc checkident('C2Manual',reseed,1)
dbcc checkident('LeadCharges',reseed,1)
dbcc checkident('AbstractSheet',reseed,1)
dbcc checkident('MeasurementSheet',reseed,1)
dbcc checkident('M_SpecificRates',reseed,1)
dbcc checkident('LabourInsurance',reseed,1)
dbcc checkident('M_MaterialRates',reseed,1)
dbcc checkident('M_LabourRate',reseed,1)
dbcc checkident('MachineManualGroup',reseed,1)
dbcc checkident('DSRDetails',reseed,1)
dbcc checkident('[DSR]',reseed,1)
