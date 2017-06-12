$in = Get-Content $triggerInput
Write-Output "Function processed queue message '$in'"

$inputParam = $in | ConvertFrom-Json


$name = $inputParam.Name

Write-Output "Output data '$name'"