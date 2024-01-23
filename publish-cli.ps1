### Run the following command to enable execution of unsigned scripts in current sessions.
### Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope Process
###


$publishingfolder = "c:\apps\Azure utils"
$singlefile = "true"

dotnet publish `
    .\cli\Azure.Cli.ContextSwitcher\Azure.Cli.ContextSwitcher.csproj `
    -p:PublishSingleFile=$singlefile,AssemblyName=azctxtst `
    --output $publishingfolder `
    --runtime win-x64 `
    --configuration "Release" `
    --no-self-contained