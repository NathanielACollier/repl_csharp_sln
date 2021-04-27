# Curl

## Linux
+ To get this sample program to run on linux I did the following
    + First you need to figure out where the `libcurl` is located at
    + Install curl, and then it should be somewhere in `/usr/lib` but it's probably in a sub folder.  So use the following commands to find it.
    ```bash
    cd /usr/lib
    find . | grep libcurl
    ```
    + Once you've found the location of libcurl you can symbolic link to it with this command.  Then dotnet will be able to find it.
    ```bash
    sudo ln -s /usr/lib/x86_64-linux-gnu/libcurl.so.4.6.0 /usr/lib/libcurl.so  
    ```
  
