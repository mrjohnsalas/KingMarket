--CustomerContacts
SELECT 
'new Product { ',
'Name = "' + [Name] + '"',
', Description = "' + [Description] + '"',
', UnitPrice = ', [UnitPrice], 'M',
', UnitCost = ', [UnitCost], 'M',
', ProductTypeId = ', [ProductTypeId],
', Stock = ', [Stock], 'M',
', MinStock = ', [MinStock], 'M',
', MaxStock = ', [MaxStock], 'M',
', UnitMeasureId = ', [UnitMeasureId],
'},' 
FROM Products 
ORDER BY ProductId