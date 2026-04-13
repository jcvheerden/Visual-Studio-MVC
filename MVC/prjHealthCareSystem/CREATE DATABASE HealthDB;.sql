CREATE DATABASE HealthDB;

use HealthDB;

-- Create Patients Table
CREATE TABLE Patients (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    ContactDetails NVARCHAR(255),
    Region NVARCHAR(100)
);

-- Create InsurancePolicies Table
CREATE TABLE InsurancePolicies (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PolicyName NVARCHAR(100) NOT NULL,
    PatientId INT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Status NVARCHAR(50) DEFAULT 'Draft',
    ServiceLevel NVARCHAR(50),
    SignedConsentPath NVARCHAR(MAX),
    CONSTRAINT FK_InsurancePolicies_Patients FOREIGN KEY (PatientId) 
        REFERENCES Patients(Id) ON DELETE CASCADE
);

-- Create Appointments Table
CREATE TABLE Appointments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    InsurancePolicyId INT NOT NULL,
    Description NVARCHAR(MAX),
    Cost DECIMAL(18, 2) NOT NULL,
    Status NVARCHAR(50),
    CONSTRAINT FK_Appointments_InsurancePolicies FOREIGN KEY (InsurancePolicyId) 
        REFERENCES InsurancePolicies(Id) ON DELETE CASCADE
);


-- Insert Dummy Patients
INSERT INTO Patients (Name, ContactDetails, Region)
VALUES 
('John Smith', 'john.smith@email.com', 'Gauteng'),
('Sarah Connor', '555-0199', 'Western Cape'),
('Thabo Mokoena', 'thabo.m@provider.co.za', 'KwaZulu-Natal');

-- Insert Dummy Insurance Policies
-- (Assuming IDs 1, 2, and 3 were generated for the patients above)
INSERT INTO InsurancePolicies (PolicyName, PatientId, StartDate, EndDate, Status, ServiceLevel)
VALUES 
('Premium Health Plus', 1, '2025-01-01', '2026-01-01', 'Active', 'Gold'),
('Basic Care', 1, '2024-06-01', '2025-06-01', 'Draft', 'Silver'),
('Family Guard', 2, '2025-03-15', '2026-03-15', 'Active', 'Platinum'),
('Student Relief', 3, '2025-01-01', '2025-12-31', 'Active', 'Bronze');

-- Insert Dummy Appointments
-- (Linking to the Policy IDs created above)
INSERT INTO Appointments (InsurancePolicyId, Description, Cost, Status)
VALUES 
(1, 'Annual Physical Examination', 1200.00, 'Completed'),
(1, 'Blood Work - Routine Check', 450.50, 'Pending'),
(3, 'Dental Wisdom Tooth Extraction', 3500.00, 'Confirmed'),
(4, 'Optometry Consultation', 850.00, 'Cancelled');
