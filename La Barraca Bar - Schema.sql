-- Crear la base de datos
CREATE DATABASE IF NOT EXISTS bar_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE bar_db;

-- Tabla de categorías
CREATE TABLE categories (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

-- Tabla de productos
CREATE TABLE products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    category_id INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (category_id) REFERENCES categories(id)
);

-- Tabla de ventas
CREATE TABLE sales (
    id INT AUTO_INCREMENT PRIMARY KEY,
    date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    total DECIMAL(10, 2) NOT NULL
);

-- Tabla de detalle de ventas
CREATE TABLE sale_items (
    id INT AUTO_INCREMENT PRIMARY KEY,
    sale_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    subtotal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (sale_id) REFERENCES sales(id),
    FOREIGN KEY (product_id) REFERENCES products(id)
);

-- Tabla de usuarios
CREATE TABLE users (
	id int AUTO_INCREMENT NOT NULL PRIMARY KEY, 
	username NVARCHAR(50) UNIQUE NOT NULL, 
	password NVARCHAR(100) NOT NULL, 
	name NVARCHAR(50) NOT NULL, 
	lastname NVARCHAR(50) NOT NULL, 
	email NVARCHAR(100) UNIQUE NOT NULL
);

-- Tabla temporal de mesas
CREATE TABLE temp_tables (
    id INT AUTO_INCREMENT PRIMARY KEY,
    table_number VARCHAR(10) NOT NULL
);

-- Tabla temporal de productos
CREATE TABLE temp_table_products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    table_id INT,
    product_name VARCHAR(100),
    quantity INT,
    FOREIGN KEY (table_id) REFERENCES temp_tables(id) ON DELETE CASCADE
);

-- Insertar usuarios
INSERT INTO users (USERNAME, PASSWORD, NAME, LASTNAME, EMAIL) VALUES
("admin", "admin1234", "Administrador", "GLS Software", "tucorreo@gmail.com");
-- Insertar categorías
INSERT INTO categories (name) VALUES
('Empanadas'),
('Pizzas'),
('Papas'),
('Tostados'),
('Cervezas'),
('Whisky'),
('Vinos y Espumantes'),
('Tragos'),
('Combos'),
('Bebidas Sin Alcohol'),
('Hamburguesas'),
('Sandwiches'),
('Lomos');

-- Insertar productos (precios estimativos)
INSERT INTO products (name, category_id, price) VALUES
-- Empanadas
('Empanada de Pollo', 1, 1800.00),
('Empanada de Carne', 1, 0.00),
('Empanada de Fugazza', 1, 1500.00),
('Empanada Especial', 1, 1500.00),
('Empanada de Choclo', 1, 0.00),
('Empanada Caprese', 1, 0.00),

-- Hamburguesas
('Hamburguesa Clásica', 11, 6000.00),
('Hamburguesa Doble', 11, 7500.00),

-- Sandwiches
('Sandwich de Milanesa', 12, 7000.00),
('Sandwich de Pollo', 12, 6500.00),

-- Lomos
('Lomo Clásico', 13, 8000.00),
('Lomo Completo', 13, 9000.00),

-- Pizzas
('Pizza Mozzarella', 2, 5000.00),
('Pizza Napolitana', 2, 5000.00),
('Pizza Fugazza', 2, 4500.00),
('Pizza Especial', 2, 0.00),
('Pizza Rúcula', 2, 0.00),
('Pizza Ternera', 2, 0.00),

-- Papas
('Papas Clásicas', 3, 4000.00),
('Papas Gratinadas', 3, 5000.00),
('Papas Cheddar', 3, 5000.00),
('Papas La Barraca', 3, 0.00),

-- Tostados
('Tostado Jamón y Queso', 4, 0.00),
('Tostado Ternera', 4, 0.00),

-- Cervezas
('Cerveza Norte', 5, 5000.00),
('Cerveza Quilmes', 5, 5500.00),
('Cerveza Heineken', 5, 8000.00),
('Cerveza Stella Artois', 5, 8000.00),
('Corona 350cc', 5, 4500.00),
('Corona 710cc', 5, 8000.00),

-- Whisky
('White Horse', 6, 0.00),
('Jack Daniels', 6, 0.00),
('Ballantines', 6, 0.00),
('Black Label', 6, 0.00),
('Red Label', 6, 0.00),

-- Vinos y Espumantes
('Cordero con Piel de Lobo', 7, 8000.00),
('Don David', 7, 17000.00),
('Trompeter', 7, 25000.00),
('Rutini', 7, 12000.00),
('Luigi Bosca', 7, 0.00),
('Santa Julia', 7, 0.00),
('Emilia', 7, 0.00),
('Los Haroldos', 7, 0.00),
('Chandon', 7, 0.00),
('Baron B', 7, 0.00),

-- Tragos
('Daiquiri Frutal', 8, 6000.00),
('Gin Clásico', 8, 6000.00),
('Gin Limón', 8, 8000.00),
('Gin Frutos Rojos', 8, 6000.00),
('Mojito', 8, 5000.00),
('Gancia', 8, 6000.00),
('Cuba Libre', 8, 5000.00),
('Fernet Pinta', 8, 4000.00),
('Fernet Medida', 8, 0.00),
('Vodka Clásico', 8, 0.00),
('Vodka Saborizado', 8, 0.00),

-- Combos
('Fernet 750cc + Coca-Cola 1L', 9, 35000.00),
('Absolut + 2 Energizantes', 9, 55000.00),
('Smirnof + 2 Energizantes', 9, 0.00),

-- Bebidas sin alcohol
('Gaseosa Coca-Cola x 1L', 10, 4000.00),
('Gaseosa Fanta x 1L', 10, 4000.00),
('Gaseosa Sprite x 1L', 10, 4000.00),
('Agua sin gas', 10, 2000.00),
('Agua con gas', 10, 2000.00),
('Agua saborizada', 10, 2500.00),
('Jugo de naranja', 10, 2500.00),
('Energizante Speed', 10, 0.00),
('Energizante Monster', 10, 0.00);
