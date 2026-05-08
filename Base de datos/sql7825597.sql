-- phpMyAdmin SQL Dump
-- version 4.7.1
-- https://www.phpmyadmin.net/
--
-- Servidor: sql7.freesqldatabase.com
-- Tiempo de generación: 08-05-2026 a las 10:20:41
-- Versión del servidor: 5.5.62-0ubuntu0.14.04.1
-- Versión de PHP: 7.0.33-0ubuntu0.16.04.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sql7825597`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mascotas`
--

CREATE TABLE `mascotas` (
  `id` int(11) NOT NULL,
  `nombre` varchar(20) NOT NULL,
  `tipo_mascota` varchar(20) NOT NULL,
  `edad` varchar(20) DEFAULT NULL,
  `size` varchar(20) DEFAULT NULL,
  `pelaje` varchar(20) DEFAULT NULL,
  `vacunas` text,
  `desparasitacion` text,
  `alimentacion` text,
  `aseo` text,
  `citas` text,
  `foto_url` varchar(200) DEFAULT NULL,
  `id_usuario` int(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `mascotas`
--

INSERT INTO `mascotas` (`id`, `nombre`, `tipo_mascota`, `edad`, `size`, `pelaje`, `vacunas`, `desparasitacion`, `alimentacion`, `aseo`, `citas`, `foto_url`, `id_usuario`) VALUES
(1, 'Luci', 'gato', '4 años', 'N/A', 'corto', NULL, NULL, NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778235451/kkjtapcclymyrjzmbsju.jpg', 1),
(13, 'Mandarina', 'gato', '7 meses', 'N/A', 'corto', NULL, NULL, NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778150518/ih2fo5se2qjqltqqsavy.jpg', 12),
(14, 'Picolo', 'gato', '5 años', 'N/A', 'corto', NULL, NULL, NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778151155/l21onvvoux4yfhzzfbw1.png', 13),
(15, 'Lloron', 'gato', '3 años', 'N/A', 'corto', NULL, NULL, NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778151532/x7lvrdiqgm5ewoh0jbeu.png', 14),
(16, 'Cuore', 'gato', '4 años', 'N/A', 'corto', '• Vacuna de demasiada belleza\n', '• Desparasitacion 02/05/2026\n', NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778220376/kokime7pg1zpnaymr77k.jpg', 15),
(17, 'Zelda', 'gato', '', 'Pequeño', 'Corto', '• Vacuna locura\n', '• Chinches\n', NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778229511/g3deomteadxztorqzilr.jpg', 17),
(18, 'Pikachu', 'gato', '5 años', 'Pequeño', 'Calvo', NULL, NULL, NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778231731/bqndyv2ahjbvygwrlxcs.png', 18),
(19, 'Rudolf', 'perro', '500 años', 'grande', 'N/A', '• Purpurina\n', '• Umpalumpas\n', NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778234650/jxtivroquwmc1cktclfm.png', 20),
(20, 'Meowth', 'gato', '', 'Pequeño', 'Corto', '• Pokemitis cronica\n', '• todas\n', NULL, NULL, NULL, 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778235203/t10qigzjrtxfef0assst.jpg', 21);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(4) NOT NULL,
  `nombre` varchar(30) NOT NULL,
  `email` varchar(40) NOT NULL,
  `password` varchar(20) NOT NULL,
  `foto_perfil` text
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id`, `nombre`, `email`, `password`, `foto_perfil`) VALUES
(1, 'Mir', 'mir@es', '1234', 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778235446/kribgxriuxtzaqcv6jia.png'),
(12, 'Miriam', 'mimoja@gmail', '1234', NULL),
(13, 'Pascual', 'pascual@gmail', '1234', NULL),
(14, 'noelia', 'noelia@gmail', '1234', NULL),
(15, 'Vic', 'victordoblem@gmail.com', '1234', 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778231528/ru4thvtqgupruca0bx0w.jpg'),
(17, 'Carlos', 'carlos@gmail.com', '1234', 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778229603/fshvdhjlnz2l0o5rc4y2.jpg'),
(18, 'Ash', 'ash@gmail.com', '1234', 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778231763/ie9ve4t1jmscrq6iycvs.png'),
(20, 'Papa Noel', 'papanoel@gmail.com', '1234', 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778234671/exsicor1etz9eii7pnyt.png'),
(21, 'Team Rocket', 'teamrocket@gmail.com', '1234', 'https://res.cloudinary.com/dql0mgp6n/image/upload/v1778235217/haeq4numjr1mru4hr56d.png');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `REL1` (`id_usuario`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;
--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD CONSTRAINT `REL1` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
