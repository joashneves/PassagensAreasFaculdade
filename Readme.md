
#  API para Sistema de Reservas de Passagens Aéreas

### Descrição

Uma Api para sistemas de reservas de passagens Aéreas, ele é uma API de um trabalho para faculdade da MULTIVIX

Desenvolvido por [Joas](https://github.com/joashneves)

## Documentação da API

#### Retorna todos os itens

```http
  GET /api/items
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `api_key` | `string` | **Obrigatório**. A chave da sua API |

#### Retorna um item

```http
  GET /api/items/${id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. O ID do item que você quer |

#### add(num1, num2)

Recebe dois números e retorna a sua soma.


## Rodando localmente

Clone o projeto

```bash
  git clone https://github.com/joashneves/PassagensAreasFaculdade.git
```

Entre no diretório do projeto

```bash
  cd PassagensAreas
```

Instale as migrations e crie no banco de dados MySQL

```bash
  .\migrar-todos-contextos.ps1
```


## Funcionalidades

- Cadastro de Passageiros
- Consulta de Voos
- Reserva de Passagens
- Cancelamento de Reservas
- Check-in
- Emissão de Bilhetes
- Relatório de Ocupação
- Relatório de Vendas
## Autores

- [@Joashneves](https://www.github.com/joashneves)

