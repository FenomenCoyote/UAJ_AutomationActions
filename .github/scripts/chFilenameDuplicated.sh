find Assets/ -type f -not -name *.meta -exec bash -c 'basename "$0" ".${0##*.*}"' {} \; sort | uniq --repeated > tmpfile
if [-s tmpfile]; then
	echo "ChFilenameDuplicated: repeated files "
	cat tmpfile
	rm -f tmpfile
	exit 1
else
	echo "ChFileNameDuplicated: success"
	rm -f tmpfile
fi
exit 0