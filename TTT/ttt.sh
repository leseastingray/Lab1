#!/bin/sh
cd /root/lab1
msbuild TicTacToe && \
mono TicTacToe.exe
