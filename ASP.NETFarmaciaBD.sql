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

-- 2. Agregar nuevas columnas
ALTER TABLE Medicamentos ADD COLUMN Descripcion TEXT;
ALTER TABLE Medicamentos ADD COLUMN FechaVencimiento DATE;
ALTER TABLE Medicamentos ADD COLUMN Marca VARCHAR(100);
ALTER TABLE Medicamentos ADD COLUMN Laboratorio VARCHAR(100);

DESCRIBE Medicamentos;

-- 3. Actualizar datos existentes con valores de ejemplo
UPDATE Medicamentos SET 
    Descripcion = 'Analgésico y antipirético',
    FechaVencimiento = '2025-12-31',
    Marca = 'Genérico',
    Laboratorio = 'Laboratorios Chile'
WHERE Nombre = 'Paracetamol 500mg';

UPDATE Medicamentos SET 
    Descripcion = 'Antiinflamatorio no esteroidal',
    FechaVencimiento = '2025-10-15',
    Marca = 'Genérico',
    Laboratorio = 'Laboratorios Chile'
WHERE Nombre = 'Ibuprofeno 400mg';

UPDATE Medicamentos SET 
    Descripcion = 'Antibiótico de amplio espectro',
    FechaVencimiento = '2025-08-20',
    Marca = 'Genérico',
    Laboratorio = 'Laboratorios Chile'
WHERE Nombre = 'Amoxicilina 500mg';

-- 4. Verificar estructura
DESCRIBE Medicamentos;