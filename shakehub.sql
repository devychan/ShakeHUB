-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 03, 2024 at 04:45 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `shakehub`
--

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `id` int(11) NOT NULL,
  `shake_id` int(11) DEFAULT NULL,
  `product` varchar(100) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `variant` varchar(50) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `status` varchar(50) DEFAULT 'pending',
  `createdAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `updatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `deletedAt` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`id`, `shake_id`, `product`, `quantity`, `variant`, `amount`, `status`, `createdAt`, `updatedAt`, `deletedAt`) VALUES
(1, 3, 'Avocado', 2, 'Regular', 70.00, 'pending', '2024-04-27 16:33:29', '2024-04-27 16:33:29', NULL),
(2, 4, 'Avocado', 4, 'Special', 276.00, 'pending', '2024-04-27 16:36:45', '2024-04-27 16:36:45', NULL),
(3, 4, 'Avocado', 23, 'Special', 1587.00, 'pending', '2024-04-28 11:39:48', '2024-04-28 11:39:48', NULL),
(4, 7, 'Milk-Chocolate', 4, 'Regular', 148.00, 'pending', '2024-05-01 01:38:59', '2024-05-01 01:38:59', NULL),
(5, 3, 'Avocado', 2, 'Regular', 78.40, 'pending', '2024-05-01 02:44:51', '2024-05-01 02:44:51', NULL),
(6, 6, 'Cookies and Cream', 2, 'Special', 168.00, 'pending', '2024-05-01 02:50:22', '2024-05-01 02:50:22', NULL),
(7, 5, 'Cookies and Cream', 2, 'Regular', 82.88, 'pending', '2024-05-01 03:12:44', '2024-05-01 03:12:44', NULL),
(8, 4, 'Avocado', 2, 'Special', 154.56, 'pending', '2024-08-03 14:01:24', '2024-08-03 14:01:24', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `shakes`
--

CREATE TABLE `shakes` (
  `id` int(11) NOT NULL,
  `flavor` varchar(100) DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `createdAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `updatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `deletedAt` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `shakes`
--

INSERT INTO `shakes` (`id`, `flavor`, `price`, `type`, `createdAt`, `updatedAt`, `deletedAt`) VALUES
(1, 'Chocolate', 35.00, 1, '2024-04-27 09:03:33', '2024-04-27 09:03:33', NULL),
(2, 'Chocolate', 69.00, 2, '2024-04-27 09:03:33', '2024-04-27 09:03:33', NULL),
(3, 'Avocado', 35.00, 1, '2024-04-27 09:03:33', '2024-04-27 09:03:33', NULL),
(4, 'Avocado', 69.00, 2, '2024-04-27 09:03:33', '2024-04-27 09:03:33', NULL),
(5, 'Cookies and Cream', 37.00, 1, '2024-04-27 09:03:33', '2024-04-27 09:03:33', NULL),
(6, 'Cookies and Cream', 75.00, 2, '2024-04-27 09:03:33', '2024-04-27 09:03:33', NULL),
(7, 'Milk-Chocolate', 37.00, 1, '2024-04-27 09:03:33', '2024-04-27 09:03:33', NULL),
(8, 'Milk-Chocolate', 75.00, 2, '2024-04-27 09:03:33', '2024-04-27 09:03:33', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `role` varchar(50) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `createdAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `updatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `variants`
--

CREATE TABLE `variants` (
  `id` int(11) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `ref` int(11) DEFAULT NULL,
  `createdAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `updatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `deletedAt` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `variants`
--

INSERT INTO `variants` (`id`, `name`, `ref`, `createdAt`, `updatedAt`, `deletedAt`) VALUES
(1, 'Regular', 1, '2024-04-27 08:56:22', '2024-04-27 08:56:22', NULL),
(2, 'Special', 2, '2024-04-27 08:56:22', '2024-04-27 08:56:22', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_shake` (`shake_id`);

--
-- Indexes for table `shakes`
--
ALTER TABLE `shakes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_variant` (`type`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `variants`
--
ALTER TABLE `variants`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `shakes`
--
ALTER TABLE `shakes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `variants`
--
ALTER TABLE `variants`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `fk_shake` FOREIGN KEY (`shake_id`) REFERENCES `shakes` (`id`);

--
-- Constraints for table `shakes`
--
ALTER TABLE `shakes`
  ADD CONSTRAINT `fk_variant` FOREIGN KEY (`type`) REFERENCES `variants` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
