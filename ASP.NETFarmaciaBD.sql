CREATE DATABASE IF NOT EXISTS FarmaciaDB;
USE FarmaciaDB;

CREATE TABLE IF NOT EXISTS ProductosFarmacia (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL
);

INSERT INTO ProductosFarmacia (Nombre, Precio, Stock) VALUES
('Paracetamol 500mg', 2.50, 100),
('Ibuprofeno 400mg', 3.20, 80),
('Amoxicilina 500mg', 5.75, 50),
('Vitamina C 1000mg', 4.90, 60),
('Jarabe para la tos', 7.30, 30);
