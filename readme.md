
# AspNet API com autenticação e autorização

Projeto com objetivo de aprendizado e conhecimento de geração de uma api com gerenciamento de usuários, utilizando sistemas de autenticação e autorização

## Tecnologias

- Csharp
- Asp Net web api
- Entity Framework (In Memory)
- Identity


## Autores

- [@ClaudineiGMS](https://github.com/claudineigms)



## Execução do projeto

Após a instalação do código na sua máquina executar os seguintes comandos em seu cmd (Dentro da pasta do projeto)

```
dotnet build
```
```
dotnet watch run
```

em seguida será aberto em seu navegador o localhost do aplicativo em Swagger Ui, em que se é necessário realizar a requisição get de ```/application/update``` para que se faça as devidas configurações e cadastro do usuário adminmistrador

realize o login pela requisição get "login/login" com as seguintes credenciais

```
Login: admin@email.com
Password: 123Aa@ 
```

# Documentação

### Login
```
- login
Realiza a conexão do usuario cadastrado na api
```
```
- logout
Corta a conexão do usuario logado na api
```

### User

- Requisito usuário com permissões de administrador
```
- register
Registra um novo usuário no banco de dados
```
```
- list
Lista todos os usuários cadastrados
```
```
- addadministrator
Adiciona permissões de administrador ao id informado
```
```
- removeadministrator
Remove permissões de administrador ao id informado
```
