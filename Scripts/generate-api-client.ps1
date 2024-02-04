
$out = "..\Source\DocuEye.Web\client-app\src\api\docueye-api"
$in = "swagger.json"
Invoke-WebRequest -Uri "https://localhost:7041/swagger/v1/swagger.json" -OutFile $in
openapi-generator-cli generate -i $in -g typescript-axios -o $out  `
--additional-properties=withSeparateModelsAndApi=true --additional-properties=modelPackage=models --additional-properties=apiPackage=api
