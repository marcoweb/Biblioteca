CREATE TABLE generos(
	id INT AUTO_INCREMENT PRIMARY KEY,
	nome VARCHAR(100) NOT NULL
);

CREATE TABLE livros(
	id INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(100) NOT NULL,
    id_genero INT NOT NULL REFERENCES generos(id)
);

CREATE TABLE clientes(
	id INT AUTO_INCREMENT PRIMARY KEY,
	nome VARCHAR(100) NOT NULL
);

CREATE TABLE locacoes(
	id INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL REFERENCES clientes(id),
    id_livro INT NOT NULL REFERENCES livros(id),
    retirada date NOT NULL,
    devolucao date DEFAULT NULL
);

INSERT INTO generos(id, nome) VALUES
	(1, 'Aventura'),
	(2, 'Ficção Científica'),
	(3, 'Romance Policial');

INSERT INTO livros(id, titulo, id_genero) VALUES
	(1, 'O Senhor dos Anéis', 1),
	(2, 'Guia do Mochileiro das Galáxias', 2),
	(3, 'Anjos e Demônios', 3),
	(4, 'Vinte Mil Léguas Submarinas', 2);

INSERT INTO clientes(id, nome) VALUES
	(1, 'Marco Carvalho'),
	(2, 'Maria Aparecida'),
	(3, 'José Souza');

INSERT INTO locacoes(id_cliente, id_livro, retirada) VALUES
	(1, 1, '2016-10-29'),
	(1, 2, '2016-10-29'),
	(2, 3, '2016-10-27'),
	(3, 4, '2016-10-28');