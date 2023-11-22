select * from library
where
(title like 'T%' 
or title like '1%')
and year between 1948 and 1960;