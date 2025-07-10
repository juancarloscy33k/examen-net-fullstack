create table clientes (
Id int primary key identity(1,1),
nombre nvarchar(100) not null,
apellido nvarchar(100) not null,
direccion nvarchar(255),
correo nvarchar(100) not null,
passwor nvarchar(255) not null
);
go 

create table tienda (
Id int primary key identity(1,1),
sucursal nvarchar(100) not null,
direccion nvarchar(255)
);
go

create table articulos(
Id int primary key identity(1,1),
codigo varchar(50) not null,
descripcion nvarchar(300),
precio decimal(18,2) not null,
imagen nvarchar(max),
stock int not null
);
go

create table articuloTienda(
articuloId int not null,
tiendaId int not null,
fecha datetime not null default getdate(),
primary key (articuloId,tiendaId),
foreign key (articuloId) references articulos(Id),
foreign key (tiendaId) references tienda(Id)
);
go

CREATE TABLE clientesArticulo (
Id INT IDENTITY(1,1) PRIMARY KEY,
clienteId INT NOT NULL,
articuloId INT NOT NULL,
tiendaId INT NULL,
Cantidad INT NOT NULL DEFAULT 1,
fecha DATETIME NOT NULL DEFAULT GETDATE(),
FOREIGN KEY (clienteId) REFERENCES clientes(Id),
FOREIGN KEY (articuloId) REFERENCES articulos(Id),
FOREIGN KEY (tiendaId) REFERENCES tienda(Id)
);
GO


select * from clientes go
select * from articulos go
select * from tienda go
select * from clientesArticulo go
select * from articuloTienda go