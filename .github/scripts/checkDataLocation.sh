mistake=0
cd Assets
find . -type f -name *.json -or -name *.txt -or -name *.xaml -or -name *.csv | while read file
do
	if [[ $file != ./Data/* ]] 
	then 
		mistake=1
		echo "file: $file is not in Data folder"	
	fi
done

if [ $mistake == 0 ]; then
	echo "All data files are correctly located"
fi

exit $mistake

