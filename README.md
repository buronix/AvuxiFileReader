# AvuxiFileReader
App Example to read letters for a given file and count them

Requisites:

Install Net Core 2 SDK

https://www.microsoft.com/net/download

If you are using Ubuntu additionally libunwind:

sudo apt-get install -y libunwind-dev

Clone the repository

git clone https://github.com/buronix/AvuxiFileReader.git

Installation:

Got to the clone repository location and navigate to the AvuxiFileReader directory

Windows

dotnet publish -c release -r win-x64

Ubuntu

dotnet publish -c release -r ubuntu.16.04-x64

Linux Portable

dotnet publish -c release -r linux-x64


Use:

Windows 

bin\Release\netcoreapp2.0\win-x64\AvuxiFileReader.exe [filePath] (Ej:exampleFile_lat.txt)

Ubuntu

Add execution permissions to executable chmod 777 bin/Release/netcoreapp2.0/ubuntu.16.04-x64/AvuxiFileReader

./bin/Release/netcoreapp2.0/ubuntu.16.04-x64/AvuxiFileReader [filePath] (Ej:exampleFile_lat.txt)
