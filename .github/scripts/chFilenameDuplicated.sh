find ./Assets/ -type f -not -name *.meta -exec bash -c 'basename "$0" ".${0##*.*}"' {} \; | sort | uniq --repeated > tmpfile
if [ -s tmpfile ]
then
	echo "ChFilenameDuplicated: Repeated files "
	cat tmpfile
	rm -f tmpfile
	exit 1
else
	echo "ChFilenameDuplicated: success"
fi
exit 0
