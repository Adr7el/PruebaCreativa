CREATE DATABASE BDPrestamos;
USE BDPrestamos;

CREATE TABLE Marca
(nomMarca VARCHAR(100) NOT NULL,
Descripcion VARCHAR(250),
TipoH VARCHAR(60),
Exactitud DECIMAL(5,2),
CONSTRAINT PK_Marca PRIMARY KEY(nomMarca)
);

CREATE TABLE Equipo
(numSerie VARCHAR(200) NOT NULL,
nMarca VARCHAR(100) NOT NULL,
nomEquipo VARCHAR(250) NOT NULL,
Descripcion VARCHAR(250),
CONSTRAINT PK_Equipo PRIMARY KEY(numSerie),
CONSTRAINT FK_Marca FOREIGN KEY(nMarca)
REFERENCES MArca(nomMarca)
ON UPDATE CASCADE
ON DELETE CASCADE
);

CREATE TABLE Prestamos
(Persona VARCHAR(50) NOT NULL,
nomMarca VARCHAR(100) NOT NULL,
nomEquipo VARCHAR(250) NOT NULL,
FechaInicio DATE,
FechaFin DATE,
Estado VARCHAR(15),
PRIMARY KEY(Persona),
FOREIGN KEY(nomMarca) REFERENCES Marca(nomMarca)
ON UPDATE CASCADE
ON DELETE CASCADE
);


INSERT INTO Marca(nomMarca, Descripcion, TipoH, Exactitud) VALUES 
('FLUKE','Industrias FLUKE','Registradores',0.9798),
('PRETUL','Industrias PRETUL','Pinzas', 0.9895),
('TRUPER','Industrias TRUPER','Pinzas', 0.9898),
('VIAKON','Industrias VIAKON','Cableado', 0.9999);

INSERT INTO Equipo(numSerie, nMarca, nomEquipo, Descripcion) VALUES 
('006545749','FLUKE','Registrador Trifasico','Registrador Trifasico de calidad electrica'),
('005142739','FLUKE','Registrador Monofasico','Registrador Monofasico de calidad de la tension'),
('005345799','PRETUL','Pinzas Ponchadoras','Pinzas para el uso de cables Ethernet'),
('005549749','TRUPER','Pinzas de corte','Pinzas para uso diario de cortes'),
('005948759','TRUPER','Pinzas pela cables','Pinzas pela cables de maxima precision'),
('005844769','VIAKON','Cables Electricos','Cables Electricos de gran fluidez');

INSERT INTO Prestamos(Persona, nomMarca, nomEquipo, FechaInicio, FechaFin, Estado) VALUES 
('Mario Enrique Ramirez Flores','FLUKE','Registrador Trifasico','30/05/2023','08/06/2023','Activo'),
('Maria Elizabeth Sandoval Ayala','PRETUL','Pinzas Ponchadoras','11/05/2023','28/05/2023','Inactivo');


SELECT * FROM Marca;
SELECT * FROM Equipo;
SELECT * FROM Prestamos;