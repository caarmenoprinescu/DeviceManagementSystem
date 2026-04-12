ALTER TABLE Devices
    ADD CONSTRAINT UQ_Devices_AllFields UNIQUE
        (
         name,
         manufacturer,
         d_type,
         os,
         os_version,
         processor,
         ram,
         description
            );