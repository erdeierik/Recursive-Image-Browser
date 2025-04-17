# Recursive Image Browser

A C# console application that recursively scans a directory, identifies image files, and generates a static HTML gallery with navigable folder structure and image paging.

---

## 📌 Features

- 📁 Recursively traverses directories
- 🖼️ Detects `.png`, `.jpg`, and `.gif` image files
- 🌐 Generates static `index.html` files for each folder containing images
- 🧭 Provides navigable links to parent and subfolders
- 🔁 Supports image paging within folders
- 🖥️ Logs success and failure messages to the console during HTML generation

---

## 🛠️ How It Works

1. The application starts from a specified root directory.
2. It recursively visits all subdirectories.
3. For each folder containing supported image files:
   - An `index.html` file is generated.
   - Images are displayed in a gallery format.
   - Links to parent and subfolders are included for navigation.
4. The program logs to the console whether each HTML generation step was successful or not.

---

## 🎓 About the Project

This project was developed as part of a university programming course.  
The main goal was to combine **recursive file system traversal** with **automated HTML generation**, while applying object-oriented principles in C#.

---

## 📸 Supported Image Formats

- `.jpg`, `.png`, `.gif`, 

---