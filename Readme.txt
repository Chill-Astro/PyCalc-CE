For Windows Export [ x64 as a Single File ] :
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

For Linux Export [ x64 as a Single File ] :
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true