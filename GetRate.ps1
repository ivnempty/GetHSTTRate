# Get UTC Timespan
$timespan=[Int64](([DateTime]::UtcNow)-(Get-Date "1/1/1970")).TotalMilliseconds

# Prepare The Request URI
$reqUri="https://rbwm-api.hsbc.com.hk/pws-hk-hase-rates-papi-prod-proxy/v1/fxtt-exchange-rates?date="+$timespan

# Invoke Web Request
$resp=Invoke-WebRequest -Uri $reqUri

# Get Response And Parse To Json
$json=$resp.Content | ConvertFrom-Json

Foreach ($rate in $json.fxttExchangeRates) { Write-Host $rate.ccyCode ": (B)" $rate.ttBuyRate "| (S)" $rate.ttSellRate }

# Get Array Item
#$jpy=$json.fxttExchangeRates | where {$_.ccyCode -eq "JPY"}
#Write-Host $jpy.ccyCode ": (B)" $jpy.ttBuyRate "| (S)" $jpy.ttSellRate

#Read-Host

Write-Host ""