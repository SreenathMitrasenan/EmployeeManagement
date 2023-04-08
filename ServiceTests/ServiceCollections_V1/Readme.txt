// Reconfigure below items to enable service layer test

Item 1	:-	Redirect or Send 
Module	:-  Controller.js 
            -> line 27 should be un commented ('res.send(data)')
            -> line 28 shuold be commented ('res.redirect('/add-user')')

// For port change
 Update port number in 
        ->Index.js 
        ->Render.js