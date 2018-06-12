CREATE DATABASE TAKEPIZZA;
USE TAKEPIZZA;

CREATE TABLE TB_USUARIO(
	ID_USUARIO		 INTEGER IDENTITY(1,1) PRIMARY KEY,
	ST_NOME			 VARCHAR(100)	 NOT NULL,
	ST_TELEFONE		 VARCHAR(20)	 NOT NULL,
	ST_EMAIL		 VARCHAR(100)	 NOT NULL,
	ST_CPF			 VARCHAR(20)	 NOT NULL,
	ST_SENHA		 VARCHAR(20)	 NOT NULL,
	DT_ADMINISTRADOR BIT DEFAULT 0	 NOT NULL
);

CREATE TABLE TB_PRODUTO(
	ID_PRODUTO		 INTEGER IDENTITY(1,1) PRIMARY KEY,
	ST_NOME			 VARCHAR(50),
	ST_TIPO			 VARCHAR(30)
)

select * from TB_PRODUTO;

--delete from TB_PRODUTO_X_TAMANHO
--delete from TB_PRODUTO

insert into TB_PRODUTO values
('Strogonoff', 'Especial', 10.00),
('Calabresa', 'Tradicional', 10.00),
('Camar�o', 'Especial', 10.00),
('Chocolate', 'Doce', 10.00);

CREATE TABLE TB_TAMANHO(
	ID_TAMANHO	 INTEGER IDENTITY(1,1) PRIMARY KEY,
	ST_NOME		 VARCHAR(15)
)

insert into TB_TAMANHO values
('pequeno'),
('medio'),
('grande'),
('gigante')

CREATE TABLE TB_PRODUTO_X_TAMANHO(
	ID_PRODXTAMANHO	 INTEGER IDENTITY(1,1) PRIMARY KEY,
	DT_PRECO_TOTAL	 DECIMAL(8,2),
	ID_PRODUTO		 INTEGER REFERENCES TB_PRODUTO,
	ID_TAMANHO		 INTEGER REFERENCES TB_TAMANHO
)

insert into TB_PRODUTO_X_TAMANHO values
(11.00, 14, 1),
(12.00, 14, 2),
(13.00, 14, 3),
(14.00, 14, 4),

(21.00, 15, 1),
(22.00, 15, 2),
(23.00, 15, 3),
(24.00, 15, 4),

(31.00, 16, 1),
(32.00, 16, 2),
(33.00, 16, 3),
(34.00, 16, 4),

(41.00, 17, 1),
(42.00, 17, 2),
(43.00, 17, 3),
(44.00, 17, 4);

CREATE TABLE TB_PEDIDO(
	ID_PEDIDO	 INTEGER IDENTITY(1,1) PRIMARY KEY,
	DT_DATA		 DATE,
	ST_TAMANHO	 VARCHAR(50),
	QNT_SABORES	 INTEGER,
	ST_STATUS	 VARCHAR(20),
	ID_USUARIO	 INTEGER REFERENCES TB_USUARIO,
	ST_CEP			 VARCHAR(9)		 NOT NULL,
	ST_RUA			 VARCHAR(100)	 NOT NULL,
	ST_NUMEROLOCAL	 INTEGER		 NOT NULL
)

CREATE TABLE TB_ITEM_PEDIDO(
	ID_ITEM				 INTEGER IDENTITY(1,1) PRIMARY KEY,
	ID_PEDIDO			 INTEGER REFERENCES TB_PEDIDO,
	ID_PRODXTAMANHO		 INTEGER REFERENCES TB_PRODUTO_X_TAMANHO,
	PRECO_ITEM			 DECIMAL(8,2)
)

-- SELECT * FROM TB_USUARIO
-- SELECT * FROM TB_PRODUTO
-- SELECT * FROM TB_TAMANHO
-- SELECT * FROM TB_PRODUTO_X_TAMANHO
-- SELECT * FROM TB_PEDIDO
-- SELECT * FROM TB_ITEM_PEDIDO