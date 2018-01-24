Create DATABASE WebApiForum

go
use WebApiForum
go
create table tbUsuarios(
id int identity primary key,
nome varchar(50) not null,
login varchar(50) not null  unique,
senha varchar(8) not null,
datacadastro datetime default getDate()
)
go
create table tbTopicos(
id int identity primary key,
titulo varchar(30) not null,
descricao varchar(100) not null,
datacadastro datetime default getDate()
)
go
create table tbPostagens(
id int identity primary key,
idTopico int foreign key references Topicos not null,
idUsuario int foreign key references Usuarios not null,
mensagem varchar(350) not null,
datapublicacao datetime default getDate()
)



INSERT INTO Usuarios (nome,login,senha) 
    values ('Fernando','corujasdev','5454545'),
        ('Edilson','edilson','454545')

select * from tbUsuarios 
select * from tbTopicos
select * from tbPostagens 

insert into tbPostagens(idTopico, idUsuario, mensagem) values(1,12,'texto')