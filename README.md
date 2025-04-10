SmartCRMShield

Overview
SmartCRMShield is a role-based transaction management web application built with ASP.NET Core. It allows users to securely manage their transactions while flagging potential fraud. Admins can monitor transactions, block/unblock fraudulent transactions, and delete transactions after password confirmation. Clients can create and view their own transactions.

Features
Role-Based Authentication:
Admin: View, block/unblock, and delete transactions.
Client: Create and view their own transactions.
Fraud Detection: Automatically flags transactions above a set amount as fraudulent.
Transaction Management: Clients can add new transactions, and Admins have the ability to manage them.
Block/Unblock: Admins can toggle the block status of a transaction if flagged as fraud.
Delete: Admins can delete transactions with password confirmation.
Technologies Used
ASP.NET Core 8 (MVC + Razor Pages)
Entity Framework Core (SQLite for database management)
Bootstrap 5 (for responsive UI)
ASP.NET Identity (for role-based authentication)
