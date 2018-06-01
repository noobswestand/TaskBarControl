/// @description Switch + Mouse



///Switch modes
#region
if keyboard_check_released(vk_space){
    mode++
    mode%=5
    
    
    switch(mode){
        case 0:
            window_normal(window_handle())
        break;
        case 1:
            window_indeterminate(window_handle())
        break;
        case 2:
            window_noprogress(window_handle())
        break;
        case 3:
            window_paused(window_handle())
        break;
        case 4:
            window_error(window_handle())
        break;
        
    }
    
}

#endregion


///Mouse
#region
//We do not need to set the value of the window if it is on
//indeterminate or no progress!
if mode!=1 && mode!=2{
    window_value(window_handle(),mouse_x,room_width)
}
#endregion

