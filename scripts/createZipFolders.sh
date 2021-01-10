#!/bin/bash

for d in "$1"/*/ ; do
  folder_path=$(readlink -f "$d");
  basename=$(basename "$folder_path");
  mkdir "$2";
  7z a "$2/$basename.zip" "$folder_path";
done
