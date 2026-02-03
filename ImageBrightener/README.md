#Image Brightening Task

## Overview

This is a console application and performs Image Brightening on a selected image file using the application interface

---

## Supported File Formats

The application supports **only** the following image formats:

- `.jpg`
- `.jpeg`
- `.png`

## Make sure the input image is in one of these formats before proceeding.

## Instructions

### 1. Launch the application

Open the Image Brightening Application and use dotnet run

### 2. Choosing options

When choosing options select numerics like 1,2,etc. and 0 (for go back) and press enter
ex:

Please choose the following

- `1` => Start Image Brightening Process
- `0` => Exit the Application

here press 1 and enter for starting the process
else press 0 and enter for exiting the program

### 3. Select Input File Path

When prompted for the **input file path**, follow the steps below:

1. Locate the image file on your system.
2. Select the file.
3. Use **one** of the following methods to copy the file path:
   - Press **Ctrl + Shift + C**
   - OR right-click the file and select **Copy file path**
4. Paste the copied path into the application when asked for the input file path.

### 4. Entering Output File Path

1. Output file path requires completePath + fileName with extension
2. You can also choose default output path generate.
3. Warning Prompt will raise if the fileName already exists asking to replace the file or enter new name.

### 5. Image Processing

1. If the mean pixel intensity is greater than 0.9 then the Image Processing will not process as the Image is already bright.
