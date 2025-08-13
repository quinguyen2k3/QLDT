import PageHeader from '@/components/PageHeader';
import { DashboardCard, mainCards ,subCards } from '@/components/DashboardCard';

function Home() {
    return (
        <>
            <PageHeader title="Trang Chủ Quản Lý Đào Tạo" />

            <section className="content">
                <div className="container-fluid">
                    <div className="row">
                        {mainCards.map((card, index) => (
                            <DashboardCard
                                key={index}
                                title={card.title}
                                subtitle={card.subtitle}
                                icon={card.icon}
                                bgColor={card.bgColor}
                                link={card.link}
                                requiredPermissions={card.requiredPermissions}
                            />
                        ))}
                    </div>
                    <div className="row">
                        {subCards.map((card, index) => (
                            <DashboardCard
                                key={index}
                                title={card.title}
                                subtitle={card.subtitle}
                                icon={card.icon}
                                bgColor={card.bgColor}
                                link={card.link}
                                requiredPermissions={card.requiredPermissions}
                            />
                        ))}
                    </div>
                </div>
            </section>
        </>
    );
}

export default Home;
