# ERPApp

A modular ERP application built using modern .NET technologies, designed for scalability, maintainability, and future extensibility.

## 🛠 Tech Stack

- **.NET Core 8.0**
- **Entity Framework Core 8.0.6**
- **SQL Server 2022**
- **IdentityServer (Duende)** + **ASP.NET Identity** for secure SSO and user management + JWT
- **AutoMapper** for object mapping
- **ClosedXML** for Excel export functionality
- **Repository Pattern** for clean and testable data access

## 📐 Architecture Overview

The application is divided into several layers:
- **API Layer**: Handles HTTP requests and responses.
- **Application Layer**: Contains business logic,Interfaces, DTOs, and services.
- **Persistance Layer**: Contains persistence logic, including EF Core and external integrations.
- **Identity Provider (IDP)**: Provides authentication and authorization using IdentityServer and ASP.NET Identity And JWT.

## 🔐 Identity & Authentication

This project implements **SSO (Single Sign-On)** using **Duende IdentityServer** integrated with **ASP.NET Identity**. The design anticipates future requirements such as:
- Multi-tenant support
- Role and claim-based authorization
- External identity providers (e.g., Google, Microsoft)

## 📦 Features

- ✅ Clean repository structure with Repository + Unit of Work patterns
- ✅ Centralized configuration management
- ✅ AutoMapper profiles for DTO ↔ Entity conversions
- ✅ Excel export via ClosedXML
- ✅ Fluent validation and error handling
- ✅ Modular service-based design (ready for microservice transition)

## 📁 Folder Structure

/ERPApp
/API → Web API application
/Application → Business logic & services & interface & Dtos 
/Entities → Domain models 
/Persistance → Database access (EF Core, repositories)
/IDP(IdentityProvider) → IdentityServer + ASP.NET Identity project

