# CSV Configuration File Explanation

This configuration files Data.csv is based on csv format file. this file time blocks where a boolean signal on modbus server is triggered (Read Coil)

### Structure Overview Data.csv

The json file contains three fields with , separtor 

### Fields Breakdown

1. **`StartTime`**:

   - **Description**: Represents the time in hour when the signal is set to true
   - **Value**: HH:MM:ss

1. **`StopTime`**:

   - **Description**: Represents the time in hour when the signal is set to false
   - **Value**: HH:MM:ss

3. **`Address`**:

   - **Description**: Address of the coil linked with the time block on modbus server .
   - **Value**: `100`, 

### Example Object

Here’s an example of csv file:

## Example

```csv
StartTime,StopTime,Address
08:00:00,09:30:00,1
10:00:00,12:00:00,2
```

## Diziscop configuration tips

### Boolean signals

Bool signals are exposed in FC-01 Read coil status zone, starting from a address 0 with Boolean Type
