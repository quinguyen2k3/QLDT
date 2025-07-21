import { useEffect, useState } from 'react';
import Header from './Header';
import SideNav from './SideNav';
import Footer from './Footer';

import Preloader from '@/components/PreLoader';

function DefaultLayout({ children }) {
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        document.body.className = 'sidebar-mini layout-fixed';
        // Giả lập tải dữ liệu hoặc chờ bootstrap
        const timer = setTimeout(() => {
            setLoading(false);
        }, 1000); // hoặc thời gian tùy ý / khi API fetch xong

        return () => {
            document.body.className = '';
            clearTimeout(timer);
        };
    }, []);

    console.log('DEFAULT LAYOUT IS RENDERING');
    
    return (
        <div className="wrapper">
            {loading && (
                <Preloader
                    src="/dist/img/logoLeVanThinhcircle.png"
                />
            )}
            <Header />
            <SideNav />
            <div className="content-wrapper">{children}</div>
            <Footer />
        </div>
    );
}

export default DefaultLayout;
