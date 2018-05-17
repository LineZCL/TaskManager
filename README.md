# Gerenciador de tarefas

Este projeto é um gerenciador de tarefas simples, onde possui apenas um usuário administrador, e ele adiciona os outros usuários do sistema. As tarefas são cadastradas por todos, sendo que o único que pode editar, deletar e atribuir as tarefas para outros usuários é o administrador. 

## Ambiente
**Linguagem:** C# 

**Frameworks:** 
* ASP.NET MVC 5
* NPoco 3 (ORM)
* Owin (Autenticação)

**Banco de Dados:**  Sql Server

**Ferramenta de testes:** Framework de teste da Microsoft

##  Estrutura do projeto
O projeto está dividido no padrão do MVC. 
* Model: Modelos referentes ao database e dataview 
* Controller: Camada de negócios. 
* View: As telas.
* Repository: Objetos de acesso ao database.
* Helpers: Objetos de uso geral. 
* SqlScript: Script para iniciar o banco de dados. 

##  Setup do projeto
1. Baixe o código no git: https://github.com/LineZCL/TaskManager
2. Cria a database com o Script que está no projeto: 
	* Solution: https://github.com/LineZCL/TaskManager/tree/master/TaskManager/SQLScript/scriptDatabase.sql

	* Test: https://github.com/LineZCL/TaskManager/blob/master/TaskManager.Tests/SQLScript/scriptDatabase.sql
3. Alterar as Connections Strings nos arquivos de configuração tanto na solution quanto no teste.
4. Rodar o projeto. 

## Melhorias futuras
Existem algumas melhorias que precisam ser feitas. 
* Mudar o Layout
* Trocar o Status da tarefa que hoje está em Enum para uma tabela no Banco de dados. 
* Melhorar tanto a lógica quanto a view de vincular uma tarefa na outra
