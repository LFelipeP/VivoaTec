# VivoaTec
# Construindo Uma API Rest em .Net

### Nesse artigo mostrarei passo a passo utilizado por mim para a criação de uma API Rest com o objetivo de cadastrar cartões virtuais.

## Sobre o Projeto:

O projeto consiste em um API que deverá cadastrar cartões virtuais de seus usuários. Ela deve receber uma request contendo um Email para cadastro e retornar o numero do cartão gerado randomicamente. 

Após o cadastro será necessário a listagem de de cartões cadastrados com seus respectivos emails organizado por ordem de cadastro para o consumidor da API.

### A API deverá conter:

- 1 End-point responsável por receber os dados de cadastro e retornar o numero do cartão cadastrado.
- 1 End-point responsável por listar os cadastros concluídos e também precisará poder receber um Email como parâmetro e com isso, devolver o cadastro correspondente.
- A implementação terá que ser em C# com .Net Core e Entity Framework Core

## Passo-a-Passo:

### Passo 1:  Criar um Projeto de API Web no Visual Studio.

Primeiro de tudo eu abri meu visual studio e iniciei um projeto API Web conforme a imagem abaixo.

![Construindo%20Uma%20API%20Rest%20em%20Net%20de2a9cfaa1634634bad5d61cc4c13754/Untitled.png](https://s3.us-west-2.amazonaws.com/secure.notion-static.com/3f21cfea-cb53-4206-a330-360bf7af5749/Untitled.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAT73L2G45O3KS52Y5%2F20210527%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20210527T185647Z&X-Amz-Expires=86400&X-Amz-Signature=b9b9019719d429312469e0dba65c5970d88ce82a94231841719e1b0048e5b145&X-Amz-SignedHeaders=host&response-content-disposition=filename%20%3D%22Untitled.png%22)

Logo após o inicio do projeto deletei alguns Templates que a Microsoft nos disponibiliza para que pudesse haver um código mais limpo.

### Passo 2: Criei a pasta Models e os modelos.

Após o primeiro passo comecei a escrever o que seria necessário para o nosso projeto. 

Primeiro criei uma classe modelo para os nossos cadastros contendo apenas os dois campos necessários, sendo eles:

- Email: Também trabalhando como chave primária o email servirá para identificar os cadastros dos cartões virtuais conforme o pedido.
- Cartão: Como parte do projeto temos que todo cadastro devera ter um cartão de crédito virtual

Assim nosso primeiro Código ficou assim:

![Imgur](https://i.imgur.com/WqGsVRU.png)

Feito isso iremos agora para o próximo passo.

### Passo 3: Adicionar um Context para o Banco de Dados

O context de banco de dados é a classe principal que coordena a funcionalidade do Entity Framework para um modelo de dados.

Utilizando o pacote *Microsoft.EntityFrameworkCore.inmemory* iremos adicionar uma classe para o context do nosso cadastro, confira o código:

![Imgur](https://i.imgur.com/jundBLb.png)

Agora com o contexto pronto temos que adicioná-lo ao nosso Startup.cs da nossa Aplicação .(Imagens Ilustrativas)

![Construindo%20Uma%20API%20Rest%20em%20Net%20de2a9cfaa1634634bad5d61cc4c13754/Untitled%203.png](https://s3.us-west-2.amazonaws.com/secure.notion-static.com/11b46b1b-9a5d-4551-a066-14d927eb5482/Untitled.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAT73L2G45O3KS52Y5%2F20210527%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20210527T194845Z&X-Amz-Expires=86400&X-Amz-Signature=459c5f3ad88247ac4ecafd2d836684e6a27e0ec079460d32bb8005f3a1e1a290&X-Amz-SignedHeaders=host&response-content-disposition=filename%20%3D%22Untitled.png%22)

### Passo 4: Criando um Controlador responsável pelas requisições web

Criando uma pasta para os controladores agora iremos criar um controlador responsável pelas requisições dos nossos cadastros

Utilizando o Visual Studio criamos uma classe controladora para que possamos lidar com os verbos HTTP requisitados nos padrões REST

O código é grande então aqui mostrarei apenas como escrevi os métodos GET e POST requisitados no projeto, confira:

1. Método GET, Listagem e Busca por Email:

    ![Imgur](https://i.imgur.com/QeHB2yq.png)

2. Método POST, Cadastro e retorno dos Cartões Virtuais:

    ![Imgur](https://i.imgur.com/WqGsVRU.png)
    
    Aqui também implementei o gerador de cartões de creditos
    
    ![Imgur](https://i.imgur.com/6KzRUBv.png)

    No método post resolvi implementar uma solução para a validação dos emails enviados conferindo a formatação dos emails enviados, veja:

    ![Construindo%20Uma%20API%20Rest%20em%20Net%20de2a9cfaa1634634bad5d61cc4c13754/Untitled%206.png](https://s3.us-west-2.amazonaws.com/secure.notion-static.com/a433e2c3-ced2-46a0-bbd4-2400a3a7d31d/Untitled.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAT73L2G45O3KS52Y5%2F20210527%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20210527T195100Z&X-Amz-Expires=86400&X-Amz-Signature=508a073b4c754207ae67bf575232422e713441f0336a4e2f909f228e07e746ca&X-Amz-SignedHeaders=host&response-content-disposition=filename%20%3D%22Untitled.png%22)

    Coloquei apenas por medidas extras para cuidado com os cadastros.

## Considerações Finais

Os End-Points estabelecidos para a comunicação foram:

1. **GET /api/Cadastros**:

    Utilizando o método GET retorna um JSON contendo todos os cadastros salvos listados em ordem de cadastros

    ![Imgur](https://i.imgur.com/YDOxagf.png)

    Ainda no /api/Cadastros é possível pesquisar apenas um cadastro passando o Email como parâmetro ficando assim: **/api/Cadastros/user@test.com**

    ![Imgur](https://i.imgur.com/OrGkNDd.png)

2. **POST /api/Cadastros:**

    Utilizando o método POST passando um Email no corpo da requisição retorna o numero do cartão virtual caso esteja tudo conforme o desejado (Figura 1) e retorna o erro 400: Bad Request caso tenha algo de errado. (Figura 2)

    Figura 1:

    ![Imgur](https://i.imgur.com/5kbsp0F.png)

    Figura 2:

    ![Construindo%20Uma%20API%20Rest%20em%20Net%20de2a9cfaa1634634bad5d61cc4c13754/Untitled%2010.png](https://s3.us-west-2.amazonaws.com/secure.notion-static.com/833cc263-5c35-4084-a17e-926f03d6c081/Untitled.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAT73L2G45O3KS52Y5%2F20210527%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20210527T195341Z&X-Amz-Expires=86400&X-Amz-Signature=060b45b8fb5e4ce15473e61dbb253ba4676256c66e255d34da4be22304a0e530&X-Amz-SignedHeaders=host&response-content-disposition=filename%20%3D%22Untitled.png%22)
