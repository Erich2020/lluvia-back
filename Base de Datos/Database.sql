CREATE DATABASE `temporada` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
use temporada;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Productos` (
  `Codigo` varchar(60) NOT NULL,
  `Descripcion` text,
  `Costo` decimal(18,2) NOT NULL,
  `Precio` decimal(18,2) NOT NULL,
  `Existencia` decimal(18,2) NOT NULL,
  PRIMARY KEY (`Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `ProductosTicket` (
  `Codigo` varchar(60) NOT NULL,
  `Descripcion` text,
  `Cantidad` decimal(18,2) NOT NULL,
  `precio` decimal(18,2) NOT NULL,
  `Importe` decimal(18,2) NOT NULL,
  PRIMARY KEY (`Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Tickets` (
  `Folio` int NOT NULL AUTO_INCREMENT,
  `ListaProductosString` text,
  `TotalArticulos` decimal(18,2) NOT NULL,
  `TotalVenta` decimal(18,2) NOT NULL,
  `Fecha` varchar(15) DEFAULT NULL,
  `Hora` varchar(15) DEFAULT NULL,
  `Fk_Usuario` int NOT NULL,
  PRIMARY KEY (`Folio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Usuarios` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(200) DEFAULT NULL,
  `Username` varchar(20) DEFAULT NULL,
  `Password` varchar(70) DEFAULT NULL,
  `Rol` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
