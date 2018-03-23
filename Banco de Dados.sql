create database DiskPizza;
use DiskPizza;

create table usuario(
	id_usuario		 integer identity(1,1) primary key,
	nome			 varchar(50)	 not null,
	sobrenome		 varchar(50)	 not null,
	tipo_telefone	 integer		 not null,
	telefone		 integer		 not null,
	email			 varchar(100)	 not null,
	conf_email		 varchar(100)	 not null,
	cpf				 varchar(11)	 not null,
	senha			 varchar(15)	 not null,
	conf_senha		 varchar(15)	 not null,
	cep				 varchar(8)		 not null,
	rua				 varchar(100)	 not null,
	n_local			 integer		 not null
);

create table bebida(
	id_bebida	 integer identity(1,1) primary key,
	nome		 varchar(30),
	preco		 decimal(8,2),
	tamanho		 varchar(30)
);

create table pizza(
	id_pizza		 integer identity(1,1) primary key,
	nome			 varchar(50),
	tamanho			 varchar(20),
	quantidade_fatia integer,
	igredientes		 varchar(200),
	preco			 decimal(8,2)
);

create table pedido(
	id_pedido	 integer identity(1,1) primary key,
	id_usuario	 integer references usuario,
	data_hora	 datetime,
);

create table item_pedido(
	id_itemP	 integer identity(1,1) primary key,
	id_pizza	 integer references pizza,
	id_pedido	 integer references pedido,
	id_bebida	 integer references bebida,
	meia_inteira varchar(10)
);

