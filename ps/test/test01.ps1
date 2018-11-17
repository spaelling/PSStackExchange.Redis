
Set-Location "C:\git\PSStackExchange.Redis"
dotnet build
Import-Module "C:\git\PSStackExchange.Redis\bin\Debug\netstandard2.0\PSStackExchange.Redis.dll"
$Password = Get-Content "$PSScriptRoot\mypassword.txt"
Connect-Multiplexer -Hostname 'PSStackExchange.redis.cache.windows.net' -Password $Password -Verbose -Passthru