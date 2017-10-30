-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Oct 31, 2017 at 12:11 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `todo_test`
--
CREATE DATABASE IF NOT EXISTS `todo_test` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `todo_test`;

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `categories_tasks`
--

CREATE TABLE `categories_tasks` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `category_id` int(11) DEFAULT NULL,
  `task_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `categories_tasks`
--

INSERT INTO `categories_tasks` (`id`, `category_id`, `task_id`) VALUES
(1, 21, 18),
(2, 22, 19),
(3, 24, 23),
(5, 26, 26),
(6, 26, 27),
(7, 30, 28),
(8, 31, 29),
(10, 34, 34),
(12, 36, 37),
(13, 36, 38),
(14, 40, 39),
(15, 41, 40),
(17, 44, 45),
(19, 46, 48),
(20, 46, 49),
(21, 50, 50),
(22, 51, 51),
(24, 54, 56),
(26, 56, 59),
(27, 56, 60),
(28, 60, 61),
(29, 61, 62),
(31, 64, 67),
(33, 66, 70),
(34, 66, 71),
(35, 70, 72),
(36, 71, 73),
(38, 74, 78),
(40, 76, 81),
(41, 76, 82),
(42, 80, 84),
(43, 81, 85),
(45, 84, 90),
(47, 86, 93),
(48, 86, 94),
(49, 90, 96),
(50, 91, 97),
(52, 94, 102),
(54, 96, 105),
(55, 96, 106),
(56, 100, 108),
(57, 101, 109),
(59, 104, 114),
(61, 106, 117),
(62, 106, 118),
(63, 110, 120),
(64, 111, 121),
(66, 114, 129),
(68, 116, 132),
(69, 116, 133),
(70, 120, 135),
(71, 121, 136),
(73, 124, 144),
(75, 126, 147),
(76, 126, 148),
(77, 130, 150),
(78, 131, 151);

-- --------------------------------------------------------

--
-- Table structure for table `tasks`
--

CREATE TABLE `tasks` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `due_date` varchar(255) DEFAULT NULL,
  `completed` tinyint(1) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `categories_tasks`
--
ALTER TABLE `categories_tasks`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `tasks`
--
ALTER TABLE `tasks`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=134;
--
-- AUTO_INCREMENT for table `categories_tasks`
--
ALTER TABLE `categories_tasks`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=80;
--
-- AUTO_INCREMENT for table `tasks`
--
ALTER TABLE `tasks`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=159;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
