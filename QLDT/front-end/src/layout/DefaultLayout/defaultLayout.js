import Header from './Header';
import SideNav from './SideNav';
import Footer from './Footer';

import Preloader from '@/components/PreLoader';

import useBodyClass from '@/hooks/Body';

function DefaultLayout({ children }) {
    useBodyClass('hold-transition sidebar-mini layout-fixed');
    
    return (
        <div className="wrapper">
            <Preloader src="/dist/img/logoLeVanThinhcircle.png"/>
            <Header />
            <SideNav />
            <div className="content-wrapper">{children}</div>
            <Footer />
        </div>
    );
}

export default DefaultLayout;
