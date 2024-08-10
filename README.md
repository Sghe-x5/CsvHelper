# JSON Object Manipulation Library and Console Application

## Overview

This project is a C# library and console application designed to parse, manipulate, and output JSON data. The library provides custom classes that represent JSON objects, along with a static parser to handle JSON data without relying on external libraries. The console application offers a user interface for interacting with the data, including features for filtering, sorting, and saving the modified data.

## Features

### Class Library
- **Custom Object Class (`MyType`)**: 
  - Represents data described in the JSON file.
  - Fields are read-only and initialized through the constructor.
  - Includes a method `ToJSON()` for converting the object back to JSON format.
  
- **Static `JsonParser` Class**:
  - Contains methods for reading and writing JSON data using streams.
  - Implements JSON parsing using regular expressions or state machines, avoiding external libraries.

### Console Application
- **Data Input**: Accepts data either through direct input in the console or by reading from a file.
- **Data Filtering**: Allows filtering of objects based on user-selected fields.
- **Data Sorting**: Supports sorting objects by a specified field, chosen by the user.
- **Data Output**: Provides options to display data in the console or save it to a file, with the ability to overwrite or specify a new file.

## Installation

1. **Clone the Repository**:
    ```sh
    git clone https://github.com/yourusername/yourproject.git
    cd yourproject
    ```

2. **Build the Project**:
    Open the solution in Visual Studio and build it to restore dependencies and compile the code.

3. **Run the Console Application**:
    Execute the application through Visual Studio or via the command line:
    ```sh
    dotnet run --project path-to-your-project
    ```

## Usage

1. **Start the Application**: Run the console application, and follow the on-screen prompts.
2. **Input Data**: Enter JSON data directly or provide a file path.
3. **Manipulate Data**: Choose options to filter or sort the data based on your needs.
4. **Save Results**: Output the processed data to the console or save it to a file.

## Development Notes

- **Code Quality**: The code adheres to C# best practices, including principles of object-oriented programming (OOP) such as encapsulation and single responsibility.
- **No External Dependencies**: The project avoids external libraries, ensuring that all functionality, including JSON parsing, is implemented manually.
- **Extensibility**: The library is designed to be extensible, allowing for easy additions or modifications while maintaining compliance with OOP principles.
