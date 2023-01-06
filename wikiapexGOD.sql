CREATE DATABASE  IF NOT EXISTS `wikiapex` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `wikiapex`;
-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: localhost    Database: wikiapex
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `habilidades`
--

DROP TABLE IF EXISTS `habilidades`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habilidades` (
  `IdHabilidades` int NOT NULL AUTO_INCREMENT,
  `NombreTactica` varchar(99) NOT NULL,
  `DescripcionTactica` mediumtext NOT NULL,
  `NombrePasiva` varchar(99) NOT NULL,
  `DescripcionPasiva` mediumtext NOT NULL,
  `NombreDefinitiva` varchar(99) NOT NULL,
  `DescripcionDefinitiva` mediumtext NOT NULL,
  PRIMARY KEY (`IdHabilidades`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `habilidades`
--

LOCK TABLES `habilidades` WRITE;
/*!40000 ALTER TABLE `habilidades` DISABLE KEYS */;
INSERT INTO `habilidades` VALUES (1,'Electroamarre','Avienta un lazo giratorio que daña y amarra al primer enemigo que se acerca demasiado.','Marca de muerte','El mapa de Ash muestra la ubicación de las cajas de muerte y marca a los atacantes sobrevivientes','Ruptura de fase','Abre un portal unidireccional hacia una ubicación objetivo.'),(2,'Neuroenlace','Los enemigos detectados por el dron de vigilancia a menos de 30 metros de tu posición quedarán marcados para que los vean tus compañeros y tú.','Dron de vigilancia','Despliega un dron aéreo que te permite ver tu entorno desde arriba. Si se destruye el dron, hay un tiempo de espera de 40 segundos hasta poder desplegar el siguiente.','PEM del dron','El dron de vigilancia libera un PEM que daña los escudos, vuelve lentos a los enemigos y desactiva las trampas.');
/*!40000 ALTER TABLE `habilidades` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `personajes`
--

DROP TABLE IF EXISTS `personajes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `personajes` (
  `Nombre` varchar(60) NOT NULL DEFAULT 'Desconocido',
  `frase` mediumtext NOT NULL,
  `seudonimo` varchar(45) NOT NULL,
  `historia` longtext NOT NULL,
  `nombrereal` varchar(45) NOT NULL DEFAULT 'Desconocido',
  `edad` varchar(45) NOT NULL DEFAULT 'Desconocido',
  `planetanatal` varchar(45) NOT NULL DEFAULT 'Desconocido',
  `url` varchar(45) NOT NULL,
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdHabilidad` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fkIdHabilidad_idx` (`IdHabilidad`),
  CONSTRAINT `fkIdHabilidad` FOREIGN KEY (`IdHabilidad`) REFERENCES `habilidades` (`IdHabilidades`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `personajes`
--

LOCK TABLES `personajes` WRITE;
/*!40000 ALTER TABLE `personajes` DISABLE KEYS */;
INSERT INTO `personajes` VALUES ('Crypto','“Es difícil asustarse cuando estás preparado”.','Experto en vigilancia','Crypto está especializado en secretos. Es un hacker brillante y un experto en cifrado y utiliza drones aéreos para espiar a sus rivales en la arena de Apex y pasar desapercibido. Pero él también tiene un secreto: se llama Tae Joon Park y se unió a los Juegos Apex para buscar a los que lo habían incriminado por asesinato.\n\nA Tae Joon lo abandonaron a una edad temprana y quedó huérfano. Consiguió escapar de una vida de miseria al convertirse en ingeniero informático para el Sindicato de mercenarios junto a su hermana adoptiva, Mila Alexander. Un día, Tae Joon y Mila se toparon con un algoritmo que podía predecir el resultado de cualquier partido de los Juegos Apex, oculto en los sistemas informáticos de los Juegos. Eso atrajo la atención de la gente equivocada: al día siguiente, Mila desapareció y Tae Joon se vio obligado a esconderse después de que lo incriminaran por su asesinato. Ahora se ha unido a los Juegos para restituir su reputación. A veces, el centro de todas las miradas es el mejor lugar para ocultarse.','Tae Joon Park','31','Gaea','https://www.youtube.com/watch?v=wP1qm7HfGuE',7,2),('Ash','“Mira tus imperfecciones en los enemigos. Luego aplástalas.”','Instigadora incisiva','Nacida en la Frontera implacable, la Dra. Ashleigh Reid, entonces aún humana, aprendió pronto que la única persona que iba a cuidar de ella era ella misma. Un día, la contrató un grupo de mercenarios para un trabajo delicado: robar una fuente de combustible experimental que se estaba investigando en Olympus, una ciudad de las Tierras Salvajes. Era el trabajo perfecto para Reid, que se infiltró y manipuló psicológicamente a los investigadores durante años. Cuando el laboratorio se autodestruyó, ella murió.\n\nY al mismo tiempo no.\n\nTransfirieron su cerebro a un simulacrum. Sin embargo, la operación le hizo perder años de recuerdos y el trauma de su \"muerte\" provocó una escisión en su personalidad. Ash es la encarnación del lado más frío, perfeccionista y ambicioso de la Dra. Reid. Sin embargo, tras esa fachada rebosante de confianza se esconde algo siniestro. Tras un encontronazo con Horizon en el que la programación del simulacrum de Ash quedó anulada, aquello que mantenía confinada la vulnerable (y no por ello menos inteligente) personalidad de Leigh ha empezado a resquebrajarse. Ash participa en los Juegos para demostrar que ha dejado atrás todo rastro de humanidad. Leigh está decidida a demostrar que se equivoca.','Dra. Ashleigh Reid','121','Desconocido','https://www.youtube.com/watch?v=CetTZ9cYPd4',8,1);
/*!40000 ALTER TABLE `personajes` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-12-27 12:15:20
