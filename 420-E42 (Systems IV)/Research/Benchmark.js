var s_date_time = new Date();
console.log('Starting: ' + s_date_time);

var i_counter = 0;

for (var $i_loop1 = 0; $i_loop1 < 10; $i_loop1++) {
   for (var $i_loop2 = 0; $i_loop2 < 32000; $i_loop2++) {
       for (var $i_loop3 = 0; $i_loop3 < 32000; $i_loop3++) {
           i_counter++;
           if (i_counter > 50) {
               i_counter = 0;
           }
       }
   } 
}

var s_date_time_end = new Date();

console.log('Counter: ' + i_counter + '\n');
console.log('End: ' + s_date_time_end + '\n');




