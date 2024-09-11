$contexts = @(
    "ClienteContext",
    "VooContext",
    "BilheteContext",
    "ReservaDePassagemContext",
    "VendaContext",
    "CheckInContext",
    "RelatorioOcupacaoContext"
)

foreach ($context in $contexts) {
    Write-Host "Criando migração para: $context"
    dotnet ef migrations add "Inicial_$context" --context $context
    Write-Host "Atualizando banco de dados para: $context"
    dotnet ef database update --context $context
}
