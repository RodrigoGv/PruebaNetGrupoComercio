CREATE DATABASE  IF NOT EXISTS `elcomercio` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `elcomercio`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: elcomercio
-- ------------------------------------------------------
-- Server version	5.5.5-10.1.30-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ordenespago`
--

DROP TABLE IF EXISTS `ordenespago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ordenespago` (
  `codOrdenPago` int(11) NOT NULL,
  `codBanco` int(11) NOT NULL,
  `codSucursal` int(11) NOT NULL,
  `monto` decimal(12,4) NOT NULL,
  `codMoneda` int(11) NOT NULL,
  `codEstado` int(11) NOT NULL DEFAULT '1',
  `fecPago` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`codOrdenPago`),
  KEY `FK_OrdenesPago_01_idx` (`codBanco`),
  KEY `FK_OrdenesPago_02_idx` (`codBanco`,`codSucursal`),
  KEY `FK_OrdenesPago_03_idx` (`codMoneda`),
  KEY `FK_OrdenesPago_04_idx` (`codEstado`),
  CONSTRAINT `FK_OrdenesPago_01` FOREIGN KEY (`codBanco`) REFERENCES `bancos` (`codBanco`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrdenesPago_02` FOREIGN KEY (`codBanco`, `codSucursal`) REFERENCES `sucursales` (`codBanco`, `codSucursal`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrdenesPago_03` FOREIGN KEY (`codMoneda`) REFERENCES `monedas` (`codMoneda`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrdenesPago_04` FOREIGN KEY (`codEstado`) REFERENCES `estados` (`codEstado`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ordenespago`
--

LOCK TABLES `ordenespago` WRITE;
/*!40000 ALTER TABLE `ordenespago` DISABLE KEYS */;
INSERT INTO `ordenespago` VALUES (1,1,1,100.0000,1,1,'2018-03-21 19:05:19'),(2,1,1,201.0000,1,1,'2018-03-21 21:23:56'),(3,3,1,50.0000,2,1,'2018-03-21 21:28:39');
/*!40000 ALTER TABLE `ordenespago` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-03-22  5:20:06
