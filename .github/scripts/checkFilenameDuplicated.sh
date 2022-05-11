find Assets/ -type f -not -name *.meta -exec bash -c 'basename "$0" ".${0##*.*}"' {} \; | sort | uniq --repeated > tmpfile

if [ -s tmpfile ]; then
	echo "Repeated Files: "
	cat tmpfile
	rm -f tmpfile
	exit 1
else 
	echo "All is good"
	rm -f tmpfile
fi

exit 0
