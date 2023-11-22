select * from library
where 
title like 'T%'
and year is not null
and title not in ('The Hunger Games','The Alchemist')
and id between 10 and 20;

