
#  API para Sistema de Reservas de Passagens Aéreas

### Descrição

Uma Api para sistemas de reservas de passagens Aéreas, ele é uma API de um trabalho para faculdade da MULTIVIX

Desenvolvido por [Joas](https://github.com/joashneves)

## Documentação da API

#### Retorna todos os itens

```http
  GET /api/VooClientes
```

Resposta: Lista de todos os voos disponíveis.

## Retorna um voo específico

```http
  GET /api/VooClientes/{id}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `di` | `int` | **Obrigatório**. O ID do voo que você deseja consultar. |

####  Consulta voos com base em parâmetros

```http
  GET /api/VooClientes/consulta
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `origem`      | `string` | **Obrigatório**.  A cidade de origem do voo. |
| `origem`      | `string` | **Obrigatório**. A cidade de destino do voo.|
| `origem`      | `DateTime` | Opcional. Data de ida do voo. |
| `origem`      | `DateTime` | Opcional. Data de volta do voo. |

Resposta: Lista de voos que atendem aos critérios fornecidos.



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

