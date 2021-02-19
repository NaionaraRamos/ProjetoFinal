# Projeto Final- Rede Social



<p align="center">
<img src="./img/instaCademy.jpg" width="50%" height="50%"/>
</p>


> Desafio do bootcamp da *We Can Code Academy - Gama Academy*


##  📌Desafio

* Cadastro de Usuario,com nome, data de nascimento, genero e foto;

* Criação de uma página de perfil para o usuario onde apareçam suas principais informaçoes, que tenha uma foto de capa,uma linha do tempo com suas postagens e onde ele possa realizar novas postagens;

* Realizar novas postagens,permitindo que o usuario acrescente textos,fotos e/ou videos em suas linhas do tempo. As postagens devem ter a funcionalidade para que os amigos dos usuarios possam curtir e comentar;

* Album de fotos que agrupe todas as fotos postadas pelo usuario em sua linha do tempo em uma galeria;

* Adicionar amigos, formando uma lista das pessoas que podem ver sua linha do tempo;

* Linha do tempo dos amigos: uma pagina que reuna os posts dos amigos,agrupados em ordem cronologica;


##  📌Techs: 
- . Net;

##  📌Instalação
- ` Git Clone` na sua maquina ;

## 📌 Rotas

*Postman:*

A API está será executada na `porta 53271` quando executada localmente.

Ou

Se utilizar o swagger a API está será executada na `porta 53271`.

### *Login*

|Método|Recurso|Utilização|
|:------:|:-------:|----------|
| POST|"/api/Login"|Faz o Login|

### *Upload*

|Método|Recurso|Utilização|
|:------:|:-------:|----------|
| POST|"/api/Upload"|Adiciona a URL de uma foto|

### *User*

|Método|Recurso|Utilização|
|:------:|:-------:|----------|
| POST|"/api/User"|Adiciona Usuario|
| GET|"/api/User/{id}"|Visualiza usuarios pelo ID|


###  *Postage*

|Método|Recurso|Utilização|
|:------:|:-------:|----------|
| POST|"/api/Postage"|Adiciona Postagem|
| POST|"/api/Postage/{id}/Comments"|Adiciona comentarios|
| POST|"/api/Postage{id}/Likes"| Dar like|
|GET|"/api/Postage"|Busca todas as postagens|
|GET|"/api/Postage/{id}/Comments"|Busca os comentarios pelo ID|
|GET|"/api/Postage{id}/Likes/Quantitaty"|Busca as quantidades de Likes pelo ID|
|GET|"/api/Postage{id}/Gallery"|Busca a galeria pelo ID|

### *Relationship*

|Método|Recurso|Utilização|
|:------:|:-------:|----------|
| POST|"/api/Relationship/RequestConnection/{idSolicitado}"|Adiciona o relacionamento|
| GET|"/api/Relationship/GetAllRelationshipRequest"|Visualiza todos os relacionamentos|
| GET|"/api/Relationship/GetAllAcceptedRequests"|Visualiza os relacionamentos aceitos|
|GET|"/api/Relationship/GetAllDeclineRequests"|Visualiza os relacionamentos que não foram aceitos|
|PATCH|"/api/Relationship/AcceptedRequests/{idSolicitante}"|Atualiza|
|PATCH|"/api/Relationship/DeclineRequests/{idSolicitante}"|Atualiza|


##  📌Contribuindo com o projeto
1. Faça o fork do projeto:https://github.com/NaionaraRamos/ProjetoFinal.git

2. Faça o checkout na branch main
`git checkout main`

3. Crie uma branch para realizar suas modificações
`git checkout -b feature/nome-da-sua-branch`

4. Após realizar as modificações, use o comando`git add .`

5. Faça o commit `git commit -m 'mensagem aqui'`

6. Faça o push `git push --set-upstream origin feature/nome-da-sua-branch`

7. Crie um novo Pull Request para a branch `feature/staging`