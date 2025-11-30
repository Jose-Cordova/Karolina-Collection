create database Karolina_Collection;
use Karolina_Collection;

-- Tablas Independientes
create table Metodo_pago(
id int primary key identity(1,1),
tipo varchar(100) not null
);
INSERT INTO Metodo_pago (tipo) VALUES 
('Efectivo'),
('Tarjeta de Crédito'),
('Tarjeta de Débito');

create table Cliente(
id int primary key identity(1,1),
nombre_completo varchar(100)not null,
dui varchar(100) not null,
telefono varchar(100) not null,
correo_electronico varchar(100) not null
);
INSERT INTO Cliente (nombre_completo, dui, telefono, correo_electronico) VALUES 
('Juan Carlos Pérez', '1234567-8', '7890-1234', 'juan.perez@email.com'),
('María Elena Gómez', '8765432-1', '7654-3210', 'maria.gomez@email.com'),
('Roberto Martínez', '1122334-4', '7555-6666', 'roberto.martinez@email.com'),
('Ana Lucía Hernández', '5566778-8', '7444-5555', 'ana.hernandez@email.com'),
('Carlos Alberto Rivas', '9988776-6', '7333-4444', 'carlos.rivas@email.com');

create table Talla(
id int primary key identity(1,1),
nombre_talla varchar(100)not null,
);
INSERT INTO Talla (nombre_talla) VALUES 
('XS'),
('S'),
('M'),
('L'),
('XL');

create table Color(
id int primary key identity(1,1),
nombre_color varchar(100)not null,
);
INSERT INTO Color (nombre_color) VALUES 
('Negro'),
('Blanco'),
('Azul'),
('Rojo'),
('Verde'),
('Amarillo'),
('Gris'),
('Rosa');

create table Categoria(
id int primary key identity(1,1),
nombre_categoria varchar(100)not null,
);
INSERT INTO Categoria (nombre_categoria) VALUES 
('Ropa Hombre'),
('Ropa Mujer'),
('Ropa Deportiva'),
('Ropa Casual');

create table Proveedor(
id int primary key identity(1,1),
nombre_proveedor varchar(100)not null,
);
INSERT INTO Proveedor (nombre_proveedor) VALUES 
('Textiles El Salvador S.A.'),
('Importadora Moda Global'),
('Distribuidora Fashion Center'),
('Textiles Premium Ltd.'),
('Proveedor Nacional de Telas');

create table Marca(
id int primary key identity(1,1),
nombre_marca varchar(100)not null,
);
INSERT INTO Marca (nombre_marca) VALUES 
('Nike'),
('Adidas'),
('Zara'),
('H&M'),
('Levi''s'),
('Tommy Hilfiger'),
('Polo Ralph Lauren');

-- Tabla para login
create table Usuario(
id int primary key identity(1,1),
nombre_usuario varchar(100)not null,
contrasenia varchar (100) not null
);
INSERT INTO Usuario (nombre_usuario, contrasenia) VALUES 
('admin', 'admin123'),
('empleado1', 'emple123');

-- Tablas Dependientes
create table Sub_categoria(
id int primary key identity(1,1),
nombre_sub_categoria varchar(100) not null,
id_categoria int,
constraint FK_Sub_categoria_Categoria foreign key (id_categoria ) references Categoria (id),
);
INSERT INTO Sub_categoria (nombre_sub_categoria, id_categoria) VALUES 
('Camisas Hombre', 1),
('Pantalones Hombre', 1),
('Camisetas Hombre', 1),
('Blusas', 2),
('Pantalones Mujer', 2),
('Vestidos', 2),
('Faldas', 2),
('Camisetas Deportivas', 3),
('Pants Deportivos', 3),
('Shorts Deportivos', 3),
('Hoodies', 4),
('Jeans', 4);

create table Producto(
id int primary key identity(1,1),
nombre_producto varchar(100) not null,
descripcion varchar(500) not null,
precio_base decimal(10,2) not null,
id_sub_categoria int,
id_marca int,
id_proveedor int,
constraint FK_Producto_Sub_categoria foreign key (id_sub_categoria) references Sub_categoria(id),
constraint FK_Producto_Marca foreign key (id_marca) references Marca(id),
constraint FK_Producto_Proveedor foreign key (id_proveedor) references Proveedor(id),
);
INSERT INTO Producto (nombre_producto, descripcion, precio_base, id_sub_categoria, id_marca, id_proveedor) VALUES 
('Camisa Formal Oxford', 'Camisa de vestir 100% algodón', 25.00, 1, 6, 1),
('Pantalón Chino Clásico', 'Pantalón casual elegante corte slim', 22.00, 2, 5, 2),
('Camiseta Basic Crew Neck', 'Camiseta básica cuello redondo', 6.00, 3, 4, 3),
('Blusa Elegante Satín', 'Blusa formal con acabado satinado', 18.00, 4, 3, 1),
('Pantalón Palazzo Fluido', 'Pantalón de pierna ancha cómodo', 24.00, 5, 3, 2),
('Vestido Cocktail Elegante', 'Vestido para ocasiones especiales', 35.00, 6, 3, 4),
('Falda Midi Plisada', 'Falda plisada longitud media', 15.00, 7, 4, 3),
('Camiseta Dri-FIT Performance', 'Camiseta deportiva tecnología anti-sudor', 18.00, 8, 1, 1),
('Pants Deportivo Training', 'Pantalón deportivo con bolsillos laterales', 25.00, 9, 2, 1),
('Shorts Running Pro', 'Shorts ligeros para correr', 15.00, 10, 1, 5),
('Hoodie Premium Cotton', 'Sudadera con capucha algodón premium', 28.00, 11, 2, 4),
('Jeans Skinny Fit', 'Jeans ajustados estilo moderno', 30.00, 12, 5, 2),
('Jeans Straight Classic', 'Jeans corte clásico tradicional', 30.00, 12, 5, 2);

create table Producto_variante(
id int primary key identity(1,1),
stock int not null,
precio_venta decimal(10,2) not null,
id_talla int,
id_color int,
id_producto int,
constraint FK_Producto_variante_Talla foreign key (id_talla) references Talla(id),
constraint FK_Producto_variante_Color foreign key (id_color) references Color(id),
constraint FK_Producto_variante_Producto  foreign key (id_producto) references Producto(id),
);
INSERT INTO Producto_variante (stock, precio_venta, id_talla, id_color, id_producto) VALUES 
-- Camisa Formal Oxford
(30, 49.99, 3, 2, 1),
(25, 49.99, 4, 2, 1),
(20, 49.99, 3, 3, 1),
(15, 49.99, 4, 3, 1),
-- Pantalón Chino
(35, 39.99, 3, 7, 2),
(30, 39.99, 4, 7, 2),
(25, 39.99, 3, 1, 2),
-- Camiseta Basic
(50, 12.99, 2, 1, 3),
(60, 12.99, 3, 1, 3),
(55, 12.99, 3, 2, 3),
(45, 12.99, 4, 6, 3),
-- Blusa Elegante
(20, 29.99, 2, 2, 4),
(25, 29.99, 3, 2, 4),
(18, 29.99, 2, 1, 4),
-- Pantalón Palazzo
(22, 45.00, 2, 1, 5),
(28, 45.00, 3, 1, 5),
(20, 45.00, 3, 3, 5),
-- Vestido Cocktail
(15, 69.99, 2, 1, 6),
(18, 69.99, 3, 4, 6),
(12, 69.99, 2, 3, 6),
-- Falda Plisada
(30, 25.00, 2, 1, 7),
(35, 25.00, 3, 6, 7),
(25, 25.00, 2, 3, 7),
-- Camiseta Deportiva
(40, 35.00, 2, 1, 8),
(45, 35.00, 3, 1, 8),
(40, 35.00, 3, 2, 8),
(35, 35.00, 4, 3, 8),
-- Pants Deportivo
(30, 49.99, 3, 1, 9),
(28, 49.99, 4, 1, 9),
(25, 49.99, 3, 6, 9),
-- Shorts Running
(50, 29.99, 2, 1, 10),
(55, 29.99, 3, 1, 10),
(45, 29.99, 3, 3, 10),
-- Hoodie Premium
(25, 49.99, 3, 1, 11),
(22, 49.99, 4, 1, 11),
(20, 49.99, 3, 6, 11),
-- Jeans Skinny
(35, 59.99, 3, 1, 12),
(30, 59.99, 4, 1, 12),
(28, 59.99, 3, 3, 12),
-- Jeans Straight
(40, 59.99, 3, 3, 13),
(35, 59.99, 4, 3, 13),
(30, 59.99, 3, 1, 13);

create table Venta(
id int primary key identity(1,1),
fecha date not null,
sub_total decimal(10,2) not null,
monto_iva decimal(10,2) not null,
total_venta decimal(10,2) not null,
id_cliente int,
id_metodo_pago int,
constraint FK_Ventas_Clientes foreign key (id_cliente) references Cliente(id),
constraint FK_Ventas_Metodo_pago foreign key (id_metodo_pago) references Metodo_pago(id),
);
INSERT INTO Venta (fecha, sub_total, monto_iva, total_venta, id_cliente, id_metodo_pago) VALUES 
('2024-11-20', 89.98, 11.70, 101.68, 1, 2),
('2024-11-21', 69.99, 9.10, 79.09, 2, 1),
('2024-11-22', 70.98, 9.23, 80.21, 3, 3),
('2024-11-23', 114.98, 14.95, 129.93, 4, 2),
('2024-11-24', 59.99, 7.80, 67.79, 5, 1);

create table Detalle_venta(
id int primary key identity(1,1),
precio_unitario decimal(10,2) not null,
cantidad int not null,
id_producto_variante int,
id_venta int,
constraint FK_Detalle_venta_Producto_variante foreign key (id_producto_variante) references Producto_variante(id),
constraint FK_Detalle_venta_Venta foreign key (id_venta) references Venta(id),
);
INSERT INTO Detalle_venta (precio_unitario, cantidad, id_producto_variante, id_venta) VALUES 
-- Venta 1
(49.99, 1, 1, 1),
(39.99, 1, 5, 1),
-- Venta 2
(69.99, 1, 18, 2),
-- Venta 3
(12.99, 2, 9, 3),
(45.00, 1, 16, 3),
-- Venta 4
(35.00, 1, 27, 4),
(49.99, 1, 37, 4),
(29.99, 1, 32, 4),
-- Venta 5
(59.99, 1, 40, 5);